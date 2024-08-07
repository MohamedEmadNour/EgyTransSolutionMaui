using EgyTransApi.Services;
using EgyTransDb.OwenSystem.DBContext.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgyTransApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAsyncController : ControllerBase
    {
        private readonly IDataAsyncService _dataAsyncService;
        public DataAsyncController(IDataAsyncService dataAsyncService)
        {
            _dataAsyncService = dataAsyncService;
        }

        [HttpGet("cardata")]
        public async Task<ActionResult<IEnumerable<CarData>>> GetAllCarDataAsync()
        {
            var cars = await _dataAsyncService.GetAllDataAsync<CarData>();
            return Ok(cars);
        }

        [HttpGet("clientdata")]
        public async Task<ActionResult<IEnumerable<ClientData>>> GetAllClientDataAsync()
        {
            var clients = await _dataAsyncService.GetAllDataAsync<ClientData>();
            return Ok(clients);
        }

        [HttpGet("clienttypevisit")]
        public async Task<ActionResult<IEnumerable<ClientTypeVisit>>> GetAllClientTypeDataAsync()
        {
            var clientTypes = await _dataAsyncService.GetAllDataAsync<ClientTypeVisit>();
            return Ok(clientTypes);
        }

        [HttpGet("driverdata")]
        public async Task<ActionResult<IEnumerable<DriverData>>> GetAllDriverDataAsync()
        {
            var drivers = await _dataAsyncService.GetAllDataAsync<DriverData>();
            return Ok(drivers);
        }

        [HttpGet("supplierdata")]
        public async Task<ActionResult<IEnumerable<SupplierData>>> GetAllSupplierDataAsync()
        {
            var suppliers = await _dataAsyncService.GetAllDataAsync<SupplierData>();
            return Ok(suppliers);
        }

        [HttpGet("traveldata")]
        public async Task<ActionResult<IEnumerable<TravelData>>> GetAllTravelDataAsync()
        {
            var travels = await _dataAsyncService.GetAllDataAsync<TravelData>();
            return Ok(travels);
        }

        [HttpGet("travelinfo")]
        public async Task<ActionResult<IEnumerable<TravelInfo>>> GetAllTravelInfoDataAsync()
        {
            var travelInfos = await _dataAsyncService.GetAllDataAsync<TravelInfo>();
            return Ok(travelInfos);
        }

        [HttpGet("tourguideclass")]
        public async Task<ActionResult<IEnumerable<TourGuideClass>>> GetAllTourGuideDataAsync()
        {
            var tourGuides = await _dataAsyncService.GetAllDataAsync<TourGuideClass>();
            return Ok(tourGuides);
        }


        [HttpPost("cardata")]
        public async Task<ActionResult> PushCarDataAsync(CarData carData)
        {
            var result = _dataAsyncService.SetDataAsync(carData);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("clientdata")]
        public async Task<ActionResult> PushClientDataAsync(ClientData clientData)
        {
            var result = _dataAsyncService.SetDataAsync(clientData);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("clienttypevisit")]
        public async Task<ActionResult> PushClientTypeDataAsync(ClientTypeVisit clientType)
        {
            var result = _dataAsyncService.SetDataAsync(clientType);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("driverdata")]
        public async Task<ActionResult> PushDriverDataAsync(DriverData driverData)
        {
            var result = _dataAsyncService.SetDataAsync(driverData);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("supplierdata")]
        public async Task<ActionResult> PushSupplierDataAsync(SupplierData supplierData)
        {
            var result = _dataAsyncService.SetDataAsync(supplierData);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("traveldata")]
        public async Task<ActionResult> PushTravelDataAsync(TravelData travelData)
        {
            var result = _dataAsyncService.SetDataAsync(travelData);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("travelinfo")]
        public async Task<ActionResult> PushTravelInfoDataAsync(TravelInfo travelInfo)
        {
            var result = _dataAsyncService.SetDataAsync(travelInfo);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("tourguideclass")]
        public async Task<ActionResult> PushTourGuideDataAsync(TourGuideClass tourGuideClass)
        {
            var result = _dataAsyncService.SetDataAsync(tourGuideClass);
            if (result.IsCompletedSuccessfully)
                return Ok(result);
            return BadRequest();
        }


    }
}
