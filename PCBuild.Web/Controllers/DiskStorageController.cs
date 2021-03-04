using PCBuild.Fatory.Repos;
using PCBuild.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PCBuild.Web.Controllers
{
    [AllowAnonymous]
    public class DiskStorageController : ApiController
    {
        private readonly IGenericRepo<DiskStorageEntity> _diskStorageRepo;
        public DiskStorageController(IGenericRepo<DiskStorageEntity> diskStorageRepo)
        {
            _diskStorageRepo = diskStorageRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> All() {
            return Ok(await _diskStorageRepo.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(DiskStorageEntity item)
        {
            if (item == null) return BadRequest("no model provided");
            return Ok(await _diskStorageRepo.CreateOrUpdate(item));
        }

    }
}
