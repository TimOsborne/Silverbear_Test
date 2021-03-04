using PCBuild.Fatory.Repos;
using PCBuild.Modles;
using System.Threading.Tasks;
using System.Web.Http;

namespace PCBuild.Web.Controllers
{
    [AllowAnonymous]
    public class MemoryController : ApiController
    {
        private readonly IGenericRepo<MemoryEntity> _memoryRepo;
        public MemoryController(IGenericRepo<MemoryEntity> memoryRepo)
        {
            _memoryRepo = memoryRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> All()
        {
            return Ok(await _memoryRepo.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(MemoryEntity item)
        {
            if (item == null) return BadRequest("no model provided");
            return Ok(await _memoryRepo.CreateOrUpdate(item));
        }
    }
}
