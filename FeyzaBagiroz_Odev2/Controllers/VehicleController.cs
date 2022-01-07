using Data.DataModel;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace FeyzaBagiroz_Odev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<WeatherForecastController> _logger;

        public VehicleController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


    


        /* Sistemdeki tüm araçları listeleyen endpoint */
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles= await _unitOfWork.Vehicle.GetAll();
            return Ok(vehicles);
        }


        /* Yeni bir araç ekleyen endpoint */

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle entity)
        {
            var response = await _unitOfWork.Vehicle.Add(entity);
            _unitOfWork.Complete();

            return Ok();

        }

        /* Araç bilgisi güncelleyen endpoint */


        [HttpPost("Update")]

        public async Task<IActionResult> UpdateVehicle([FromBody] Vehicle entity)
        {
            var response = await _unitOfWork.Vehicle.Update(entity);
            _unitOfWork.Complete();


         
            return Ok();

        }
         /*Aracı silen endpoint / Aracı silerken araca bağlı containerlarıda silecektir */

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleById(long id)
        {

            var result = _unitOfWork.Container.Where(x => x.VehicleId == id).ToList();

            if (result.Count != 0)
            {

                foreach (var item in result)
                {
                    var aa=  _unitOfWork.Container.Delete(item.Id);
                    

                }
            }
            
            var response = _unitOfWork.Vehicle.Delete(id);

            _unitOfWork.Complete();

            return Ok();

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
