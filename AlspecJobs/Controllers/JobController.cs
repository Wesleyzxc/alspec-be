
namespace AlspecBackend.Controllers
{
    using AlspecBackend.DTO;
    using AlspecBackend.Projections;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Produces("application/json")]
    [Route("api/jobs")]
    public class JobController : Controller
    {
        private readonly DataContext context;
        private readonly ILogger<JobController> logger;

        public JobController(DataContext context, ILogger<JobController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobDTO job)
        {
            try
            {
                var entity = job.ToEntity();
                context.Add(entity);
                await context.SaveChangesAsync();

                return Ok(job);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex}");
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(JobDTO[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Getting all jobs");
            try
            {
                var jobs = context.Jobs.Include(j => j.SubItems).Select(j => j.ToDto()).ToList();

                return Ok(jobs);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: ${ex}");
                return NoContent();
            }
        }

    }
}