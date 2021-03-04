using PCBuild.Fatory.Repos;
using System;
using System.Collections.Generic;
using PCBuild.Modles;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PCBuild.Web.Helpers;

namespace PCBuild.Web.Controllers
{
    [AllowAnonymous]
    public class PcBuildsController : ApiController
    {
        private readonly IPCBuildsRepository _pcBuildsRepository;        
        private readonly IGenericRepo<UsbToBuildEntity> _usbToBuildRepo;

        public PcBuildsController(IPCBuildsRepository pcBuildsRepository, IGenericRepo<UsbToBuildEntity> usbToBuildRepo = null)
        {
            _pcBuildsRepository = pcBuildsRepository;
            _usbToBuildRepo = usbToBuildRepo;
        }

        // GET: PcBuilds
        [HttpGet]
        public async Task<IHttpActionResult> ActiveBuilds()
        {            
            return  Ok(await _pcBuildsRepository.GetAll());
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddBuild(NewPcBuild newPcBuild) {

            if (newPcBuild == null) return BadRequest("no model provided");
            PCBuildsEntity newUpdateItem = new PCBuildsEntity();


            newUpdateItem.Memory = newPcBuild.Memory;
            newUpdateItem.DiskStorage = newPcBuild.DiskStorage;
            newUpdateItem.Graphics = newPcBuild.Graphics;
            newUpdateItem.PowerSupply = newPcBuild.PowerSupply;
            newUpdateItem.PcWeight = newPcBuild.PcWeight;
            newUpdateItem.Processors = newPcBuild.Processors;

            if (newPcBuild.Id != 0)
            {
                newUpdateItem.Id = newPcBuild.Id;                
                newUpdateItem = await _pcBuildsRepository.Update(newUpdateItem);
            }
            else
                newUpdateItem = await _pcBuildsRepository.Create(newUpdateItem);
            

            var checkedUsbId = new List<int>();
            for (var i = 0; i < newPcBuild.Usbs.Count(); i++)
            {

                UsbToBuildEntity UsbToBuild = new UsbToBuildEntity();

                var x = newPcBuild.Usbs[i];
                if (x.Id != 0)
                {
                    UsbToBuild = await _usbToBuildRepo.Get(x.Id);
                    UsbToBuild.Usb = x.Usb;
                    UsbToBuild.PcBuild = newUpdateItem;
                    UsbToBuild.NumberOf = x.NumberOf;
                    UsbToBuild = await _usbToBuildRepo.Update(UsbToBuild);
                }
                else {
                    UsbToBuild.Usb = x.Usb;
                    UsbToBuild.PcBuild = newUpdateItem;
                    UsbToBuild.NumberOf = x.NumberOf;
                    UsbToBuild = await _usbToBuildRepo.Create(UsbToBuild);
                };
                checkedUsbId.Add(UsbToBuild.Id);

            }

            var UsbData =  await _usbToBuildRepo.Get(x => x.PcBuild.Id == newPcBuild.Id);

            for (var i = 0; i < UsbData.Count(); i++)
            {
                if (checkedUsbId.Contains(UsbData[i].Id))
                    continue;
                else
                    await _usbToBuildRepo.Delete(UsbData[i]);                
            }
            newUpdateItem.USBs = await _usbToBuildRepo.Get(x => x.PcBuild.Id == newPcBuild.Id);



            return Ok(newUpdateItem);

        }        
    }
}
