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
        protected readonly IRepository<T> _repository;

        protected BaseController(IRepository<T> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(T item)
        {
            try
            {
                Validate();

                var result = await _repository.UpdateAsync(item);

                return Ok(result);
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
