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
    public class PowerSupplyController : ApiController
    {
        private readonly IGenericRepo<PowerSupplyEntity> _powerSupplyRepo;
        public PowerSupplyController(IGenericRepo<PowerSupplyEntity> powerRepo)
        {
            _powerSupplyRepo = powerRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> All()
        {
            return Ok(await _powerSupplyRepo.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(PowerSupplyEntity item)
        {
            if (item == null) return BadRequest("no model provided");
            return Ok(await _powerSupplyRepo.CreateOrUpdate(item));
        }
    }
}
