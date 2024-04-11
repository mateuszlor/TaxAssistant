using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
    public abstract class BaseController<T> : ControllerBase
        where T : BaseModel
    {
        private readonly ILogger<BaseController<T>> _logger;
        private readonly BaseRepository<T> _repository;

        public BaseController(
            ILogger<BaseController<T>> logger,
            BaseRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var data = await _repository.GetAsync(id);

                if (data == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex) when (ex.InnerException is Exception innerException)
            {
                var error = new Error
                {
                    Message = innerException.Message,
                    Type = innerException.GetType().Name
                };

                return BadRequest(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Type = ex.GetType().Name
                };

                return BadRequest(error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _repository.GetAllAsync();

                if (data == null || !data.Any())
                {
                    return StatusCode(204, new List<T>());
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex) when (ex.InnerException is Exception innerException)
            {
                var error = new Error
                {
                    Message = innerException.Message,
                    Type = innerException.GetType().Name
                };

                return BadRequest(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Type = ex.GetType().Name
                };

                return BadRequest(error);
            }
        }
    }
}
