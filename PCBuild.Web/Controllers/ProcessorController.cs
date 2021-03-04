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
    public class ProcessorController : ApiController
    {
        private readonly IGenericRepo<ProcessorEntity> _processorRepo;
        public ProcessorController(IGenericRepo<ProcessorEntity> processorRepo)
        {
            _processorRepo = processorRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> All()
        {
            return Ok(await _processorRepo.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(ProcessorEntity item)
        {
            if (item == null) return BadRequest("no model provided");
            return Ok(await _processorRepo.CreateOrUpdate(item));
        }
    }
}
