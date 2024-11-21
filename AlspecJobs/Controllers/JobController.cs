
namespace AlspecBackend.Controllers
{
    using AlspecBackend.DTO;
    using AlspecBackend.Entities;
    using AlspecBackend.Projections;
    using AlspecBackend.Repository;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces("application/json")]
    [Route("api/jobs")]
    public class JobController : Controller
    {
        private readonly DataContext context;
        private readonly ILogger<JobController> logger;
        private readonly IRepository<Job> jobRepository;

        public JobController(DataContext context, ILogger<JobController> logger, IRepository<Job> jobRepository)
        {
            this.context = context;
            this.logger = logger;
            this.jobRepository = jobRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobDTO job)
        {
            try
            {
                if (job.SubItems.Any(s => s.JobId != job.Id))
                {
                    return BadRequest("A SubItem does not belong to the current job.");
                }

                var entity = job.ToEntity();

                await jobRepository.AddAsync(entity);

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
                var jobEntities = await jobRepository.GetAll();
                var jobs = jobEntities.Select(j => j.ToDto()).ToList();

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