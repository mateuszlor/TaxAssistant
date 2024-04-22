using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Adapter;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Database;
using TaxAssistant.JPK.Shared.Model.Xml.JPK_EWP;
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
        private readonly KpirAdapter _kpirAdapter;
        private readonly KpirRepository _kpirRepository;
        private readonly EwpAdapter _ewpAdapter;
        private readonly EwpRepository _ewpRepository;
        private readonly ImportRepository _importRepository;

        public ImportController(
            ILogger<ImportController> logger,
            KpirAdapter kpirAdapter,
            KpirRepository kpirRepository,
            EwpAdapter ewpAdapter,
            EwpRepository ewpRepository,
            ImportRepository importRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _kpirAdapter = kpirAdapter ?? throw new ArgumentNullException(nameof(kpirAdapter));
            _kpirRepository = kpirRepository ?? throw new ArgumentNullException(nameof(kpirRepository));
            _ewpAdapter = ewpAdapter ?? throw new ArgumentNullException(nameof(ewpAdapter));
            _ewpRepository = ewpRepository ?? throw new ArgumentNullException(nameof(ewpRepository));
            _importRepository = importRepository ?? throw new ArgumentNullException(nameof(importRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportResult), 200)]
        [ProducesResponseType(typeof(ImportResult), 400)]
        public async Task<IActionResult> Import([FromBody] string content)
        {
            var result = Deserialize(content);

            if (result is Error error)
            {
                var model = new ImportResult
                {
                    IsSuccessful = false,
                    Error = error
                };

                return BadRequest(model);
            }
            else
            {
                var model = new ImportResult
                {
                    IsSuccessful = true,
                };

                switch (result)
                {
                    case JPK_PKPIR kpir:
                    {
                        var items = _kpirAdapter.Adapt(kpir);
                        var addedItems = await _kpirRepository.AddAsync(items);

                        var importData = new Import
                        {
                            KpirId = addedItems.Id
                        };

                        var addedImportData = await _importRepository.AddAsync(importData);

                        model.Data = addedImportData;

                        break;
                    }
                    case JPK_EWP ewp:
                    {
                        var items = _ewpAdapter.Adapt(ewp);
                        var addedItems = await _ewpRepository.AddAsync(items);

                        var importData = new Import
                        {
                            EwpId = addedItems.Id
                        };

                        var addedImportData = await _importRepository.AddAsync(importData);

                        model.Data = addedImportData;

                        break;
                    }

                }

                return Ok(model);
            }
        }

        private IDictionary<string, Type> _namespaces = new Dictionary<string, Type>
        {
            { "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/", typeof(JPK_PKPIR) },
            { "http://crd.gov.pl/wzor/2020/05/08/9393/", typeof(JPK_V7M_1) },
            { "http://crd.gov.pl/wzor/2021/12/27/11148/", typeof(JPK_V7M_2) },
            { "http://jpk.mf.gov.pl/wzor/2022/02/01/02011/", typeof(JPK_EWP) }
        };

        private object Deserialize(string content)
        {
            try
            {
                var xmlString = HttpUtility.HtmlDecode(content);

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);

                if (!_namespaces.TryGetValue(xmlDocument.DocumentElement.NamespaceURI, out var type))
                {
                    throw new NotImplementedException();
                }

                var serializer = new XmlSerializer(type);
                var reader = new StringReader(xmlString);
                var model = serializer.Deserialize(reader);

                return model;
            }
            catch (Exception ex) when (ex.InnerException is Exception innerException)
            {
                return new Error { Message = innerException.Message, Type = innerException.GetType().Name };
            }
            catch (Exception ex)
            {
                return new Error { Message = ex.Message, Type = ex.GetType().Name };
            }
        }
    }
}
