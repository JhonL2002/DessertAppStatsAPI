using DessertAppDevOpsStatistics.Services;
using Microsoft.AspNetCore.Mvc;

namespace DessertAppDevOpsStatistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelinesController : ControllerBase
    {
        private readonly PipelineService _pipelineService;

        public PipelinesController(PipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        [HttpGet("pipelines")]
        public async Task<IActionResult> GetSuccesfulPipelines()
        {
            try
            {
                var pipelines = await _pipelineService.GetSuccessfulPipelines();
                if (pipelines == null || pipelines.Count == 0)
                {
                    return NotFound("No successful pipelines found");
                }
                return Ok(pipelines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
