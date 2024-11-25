using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TaxAssistant.JPK.ApplicationLogic.Repository.Abstraction;
using TaxAssistant.JPK.Shared.Model;
using TaxAssistant.JPK.Shared.Model.Abstraction;

namespace TaxAssistant.JPK.Server.Controllers.Data
{
	public abstract class BaseController<T> : ControllerBase
        where T : BaseModel
    {
        private readonly ILogger<BaseController<T>> _logger;
        private readonly IRepository<T> _repository;

        protected BaseController(
            ILogger<BaseController<T>> logger,
            IRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                Validate();

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
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Validate();

                await _repository.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
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
                    return NoContent();
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private void Validate()
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException($"Validation errors: {string.Join(";", ModelState.Values.SelectMany(v => v.Errors))}");
            }
        }

        private IActionResult HandleError(Exception ex)
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
