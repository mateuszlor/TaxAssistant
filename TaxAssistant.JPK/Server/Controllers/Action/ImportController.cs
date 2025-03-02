using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Database.Ewp;
using TaxAssistant.JPK.Shared.Model.Database.Fa;
using TaxAssistant.JPK.Shared.Model.Database.Kpir;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_EWP;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_FA;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_PKPIR;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_1;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_V7M_2;

namespace TaxAssistant.JPK.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IJpkAdapter<JPK_PKPIR, Kpir> _kpirAdapter;
        private readonly IRepository<Kpir> _kpirRepository;
        private readonly IJpkAdapter<JPK_EWP, Ewp> _ewpAdapter;
        private readonly IRepository<Ewp> _ewpRepository;
        private readonly IJpkAdapter<JPK_FA, Fa> _faAdapter;
        private readonly IRepository<Fa> _faRepository;
        private readonly IRepository<Import> _importRepository;

        public ImportController(
            ILogger<ImportController> logger,
            IJpkAdapter<JPK_PKPIR, Kpir> kpirAdapter,
            IRepository<Kpir> kpirRepository,
            IJpkAdapter<JPK_EWP, Ewp> ewpAdapter,
            IRepository<Ewp> ewpRepository,
            IJpkAdapter<JPK_FA, Fa> faAdapter,
            IRepository<Fa> faRepository,
            IRepository<Import> importRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _kpirAdapter = kpirAdapter ?? throw new ArgumentNullException(nameof(kpirAdapter));
            _kpirRepository = kpirRepository ?? throw new ArgumentNullException(nameof(kpirRepository));
            _ewpAdapter = ewpAdapter ?? throw new ArgumentNullException(nameof(ewpAdapter));
            _ewpRepository = ewpRepository ?? throw new ArgumentNullException(nameof(ewpRepository));
            _faAdapter = faAdapter ?? throw new ArgumentNullException(nameof(faAdapter));
            _faRepository = faRepository ?? throw new ArgumentNullException(nameof(faRepository));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportResult), 200)]
        [ProducesResponseType(typeof(ImportResult), 400)]
        public async Task<IActionResult> Import([FromBody] string content)
        {
            try
            {
                var result = Deserialize(content);

                var model = new ImportResult
                {
                    IsSuccessful = true,
                };

                Guid? kpirId = null;
                Guid? ewpId = null;
                Guid? faId = null;

                switch (result)
                {
                    case JPK_PKPIR kpir:
                    {
                        var items = _kpirAdapter.Adapt(kpir);
                        var addedItems = await _kpirRepository.AddAsync(items);
                        kpirId = addedItems.Id;

                        break;
                    }
                    case JPK_EWP ewp:
                    {
                        var items = _ewpAdapter.Adapt(ewp);
                        var addedItems = await _ewpRepository.AddAsync(items);
                        ewpId = addedItems.Id;

                        break;
                    }
                    case JPK_FA fa:
                    {
                        var item = _faAdapter.Adapt(fa);
                        var added = await _faRepository.AddAsync(item);
                        faId = added.Id;

                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException($"No adapter for {result.GetType().Name}");
                    }
                }

                var importData = new Import
                {
                    KpirId = kpirId,
                    EwpId = ewpId,
                    FaId = faId
                };

                var addedImportData = await _importRepository.AddAsync(importData);

                model.Data = addedImportData;

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adapting JPK file content");

                var error = new Error
                {
                    Message = ex.Message,
                    Type = ex.GetType().Name
                };

                var response = new ImportResult
                {
                    IsSuccessful = false,
                    Error = error
                };

                return BadRequest(response);
            }
        }

        private IDictionary<string, Type> _namespaces = new Dictionary<string, Type>
        {
            { "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/", typeof(JPK_PKPIR) },
            { "http://crd.gov.pl/wzor/2020/05/08/9393/", typeof(JPK_V7M_1) },
            { "http://crd.gov.pl/wzor/2021/12/27/11148/", typeof(JPK_V7M_2) },
            { "http://jpk.mf.gov.pl/wzor/2022/02/01/02011/", typeof(JPK_EWP) },
            { "http://jpk.mf.gov.pl/wzor/2022/02/17/02171/", typeof(JPK_FA) }
        };

        private object Deserialize(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Empty JPK file content");
            }

            var xmlString = HttpUtility.HtmlDecode(content);

            var xmlDocument = new XmlDocument();

            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit,
                XmlResolver = null
            };

            using var xmlReader = XmlReader.Create(new StringReader(xmlString), settings);
            xmlDocument.Load(xmlReader);

            if (string.IsNullOrEmpty(xmlDocument.DocumentElement?.NamespaceURI))
            {
                throw new NotImplementedException($"XML has no namespace");
            }

            if (!_namespaces.TryGetValue(xmlDocument.DocumentElement.NamespaceURI, out var type))
            {
                throw new NotImplementedException($"Namespace \"{xmlDocument.DocumentElement.NamespaceURI}\" has no handler");
            }

            var serializer = new XmlSerializer(type);
            var stringReader = new StringReader(xmlString);
            var model = serializer.Deserialize(stringReader);

            return model;
        }
    }
}
