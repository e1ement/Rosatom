using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Dto;

namespace Rosatom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public WorksController(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid? id)
        {
            try
            {
                var data = await _repository.WorkRepository.GetByIdAsync(id, false);
                var result = data.OrderBy(o => o.SumAddedCost);
                
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in the {nameof(Get)} action {e}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] WorkForUpdateDto workForUpdate)
        {
            try
            {
                var result = await _repository.WorkRepository.UpdateAsync(workForUpdate);
                if (result == 0)
                {
                    return BadRequest();
                }

                await _repository.WorkRepository.Recalculate();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in the {nameof(Update)} action {e}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateData()
        {
            try
            {
                var data = _repository.WorkRepository.GenerateData();
                await _repository.WorkRepository.CreateCollectionAsync(data);

                return Created(string.Empty, null);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateData)} action {e}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
