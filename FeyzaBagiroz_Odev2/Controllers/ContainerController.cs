using Data.DataModel;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeyzaBagiroz_Odev2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<WeatherForecastController> _logger;

        public ContainerController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /* Sistemdeki tüm containerları listeleyen endpoint */ 

        [HttpGet]
        public async Task<IActionResult> GetAllContainers()
        {
            var containers = await _unitOfWork.Container.GetAll();
            return Ok(containers);
        }

        /* Sisteme yeni bir container ekleyen endpoint */
        [HttpPost]
        public async Task<IActionResult> AddContainer([FromBody] Container entity)
        {

            var response = await _unitOfWork.Container.Add(entity);
            _unitOfWork.Complete();

            return Ok();

        }

        /* Container bilgisini güncelleyen api / güncelleme sırasında vehicle ıd güncellenmeyecektir */

        [HttpPost("Update")]

        public async Task<IActionResult> UpdateContainer([FromBody] ContainerViewModel entity)
        {
            //Burada vehicleId alanının günellenmemesi için bir viewmodel oluşturdum ve parametre olarak bunu gönderdim
            //parametre olarak gönderilen id nin boş olma ve ya sistemde olmama durumu control edildi
            if (entity != null && entity.Id > 0)
            {
                var result = await _unitOfWork.Container.GetById(entity.Id);
                if (result != null)
                {
                  //ContainerName alanı boş değil ise güncelleme işlemi yapacak
                  if (!string.IsNullOrWhiteSpace(entity.ContainerName))
                        result.ContainerName = entity.ContainerName;

                    result.Latitude = entity.Latitude;
                    result.Longitude = entity.Longitude;

                    var response = await _unitOfWork.Container.Update(result);
                    _unitOfWork.Complete();


                    return Ok();
                }

            }
            return NotFound();
        }

        /* Container silecek endpoint */

        [HttpDelete]
        public async Task<IActionResult> DeleteContainerById(long id)
        {
            var response = await _unitOfWork.Container.Delete(id);
            _unitOfWork.Complete();

            return Ok();

        }


         /* VEHİCLEID İLE İSTEK YAPILDIĞINDA O ARACA AİT TÜM CONTAİNERLARI LİSTELEYEN APİ */

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAllByVehicleId(long id)
        //{
        //    var result =  _unitOfWork.Container.Where(x=>x.VehicleId == id).ToList();

        //    if (result is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}




        /* VehicleId ve n parametresi alarak araca ait containerlerı eşit eleman olacak şekilde n kümeye ayırıp tek response döndüre api
   */
        [HttpGet("{vehicleId} / {n}")]
        public async Task<IActionResult> GroupByContainer(long vehicleId, int n)
        {
            var list = new List<List<Container>>();
            var result = _unitOfWork.Container.Where(x => x.VehicleId == vehicleId).ToList();
            var elementCount = result.Count / n;
            var index = 0;
            for (int i = 0; i < n; i++)
            {
                list.Add(result.GetRange(index, elementCount));
                index += elementCount;
            }

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(long id)
        //{
        //    var result = await _unitOfWork.Container.GetById(id);

        //    if (result is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}












    }
}
