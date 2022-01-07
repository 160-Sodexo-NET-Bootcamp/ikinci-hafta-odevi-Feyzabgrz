using Data.DataModel;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FeyzaBagiroz_Odev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


    


        /* Sistemdeki tüm araçları listeleyen endpoint */
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {

            try
            {
                var result = await _unitOfWork.Vehicle.GetAll();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /* Yeni bir araç ekleyen endpoint */

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle entity)
        {
            try
            {
                var result = await _unitOfWork.Vehicle.Add(entity);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /* Araç bilgisi güncelleyen endpoint */


        [HttpPost("Update")]

        public async Task<IActionResult> UpdateVehicle([FromBody] Vehicle entity)
        {

            try
            {
                var result = await _unitOfWork.Vehicle.Update(entity);
                _unitOfWork.Complete();
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
         /*Aracı silen endpoint / Aracı silerken araca bağlı containerlarıda silecektir */

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleById(long id)
        {

            try
            {
                var result = _unitOfWork.Container.Where(x => x.VehicleId == id).ToList();

                if (result.Count != 0)
                {

                    foreach (var item in result)
                    {
                        var aa = _unitOfWork.Container.Delete(item.Id);


                    }
                }

                var response = _unitOfWork.Vehicle.Delete(id);

                _unitOfWork.Complete();

                return new JsonResult(response.Result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

   



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _unitOfWork.Vehicle.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
     
    }
}
