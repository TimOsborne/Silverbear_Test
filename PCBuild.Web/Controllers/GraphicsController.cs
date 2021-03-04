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
    public class GraphicsController : ApiController
    {
        private readonly IGenericRepo<GraphicsEntity> _graphicsRepo;
        public GraphicsController(IGenericRepo<GraphicsEntity> graphicsRepo)
        {
            _graphicsRepo = graphicsRepo;
        }

        [HttpGet]
        public async Task<IHttpActionResult> All()
        {
            return Ok(await _graphicsRepo.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(GraphicsEntity item)
        {
            if (item == null) return BadRequest("no model provided");
            return Ok(await _graphicsRepo.CreateOrUpdate(item));
        }
    }
}
