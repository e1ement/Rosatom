using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong in the {nameof(Get)} action {e}");
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
