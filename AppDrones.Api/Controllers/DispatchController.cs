using AppDrones.Core.Dto;
using AppDrones.Core.Exceptions;
using AppDrones.Core.Extensions;
using AppDrones.Core.Interfaces;
using AppDrones.Core.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AppDrones.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDrone repo;
        private readonly IValidator<RegistryReqDto> registryValidator;
        private readonly IValidator<IEnumerable<LoadMedicationReqDto>> loadMedicationValidator;
        public DispatchController(IDrone drone, IValidator<RegistryReqDto> _registryValidator, IValidator<IEnumerable<LoadMedicationReqDto>> loadMedicationValidator)
        {
            repo = drone;
            registryValidator = _registryValidator;
            this.loadMedicationValidator = loadMedicationValidator;
        }

        /// <summary>
        /// Registering a drone.
        /// </summary>
        /// <param name="drone">Drone to registry</param>
        /// <response code="201">Returns the drone created.</response>
        /// <response code="400">Invalid drone input parameter.</response>
        /// <response code="500">Internal Server error.</response>
        [ProducesResponseType(typeof(RegistryResDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("registry")]
        public async Task<ActionResult<RegistryResDto>> PostRegistry(RegistryReqDto drone)
        {
            try
            {
                ValidationResult result = await registryValidator.ValidateAsync(drone);
                if (result.IsValid)
                {
                    var response = await repo.Registry(drone);
                    if (response != null)
                        return Created(string.Empty, drone);
                    else
                        return BadRequest();
                }
                else
                {
                    result.AddToModelState(this.ModelState);
                    var vl = new ValidationProblemDetails(this.ModelState);
                    return BadRequest(vl);
                }
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Loading a drone with medication items.
        /// </summary>
        /// <param name="medications">List of medication to load on the drone</param>
        /// <param name="droneId">Id of the drone</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPatch("{droneId}/loading")]
        public async Task<ActionResult> PatchLoadingDrone(IEnumerable<LoadMedicationReqDto> medications, int droneId)
        {
            try
            {
                ValidationResult result = await loadMedicationValidator.ValidateAsync(medications);
                if (result.IsValid)
                {
                    var response = await repo.LoadingMedication(medications, droneId);
                    if (response)
                        return NoContent();
                    else
                        return BadRequest();
                }
                else
                {
                    result.AddToModelState(this.ModelState);
                    var vl = new ValidationProblemDetails(this.ModelState);
                    return BadRequest(vl);
                }
            }
            catch (Exception e)
            {
                if (e is WeightLimitException || e is DroneNotFoundException || e is DroneStatusException || e is LowBatteryException)
                {
                    this.ModelState.TryAddModelError(e.Source!, e.Message);
                    var vl = new ValidationProblemDetails(this.ModelState);
                    return BadRequest(vl);
                }
                else
                    return Problem();
            }
        }

        /// <summary>
        /// Checking loaded medication items for a given drone.
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        [ProducesResponseType(typeof(IEnumerable<LoadedMedicationsResDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("{droneId}/medications")]
        public async Task<ActionResult> GetMedicationsByDrone(int droneId)
        {
            try
            {
                var response = await repo.LoadedMedications(droneId);
                if (response.Count() == 0)
                {
                    return NotFound();
                }
                else
                    return Ok(response);
            }
            catch (Exception e)
            {
                if (e is DroneNotFoundException)
                {
                    this.ModelState.TryAddModelError(e.Source!, e.Message);
                    var vl = new ValidationProblemDetails(this.ModelState);
                    return BadRequest(vl);
                }
                else
                    return Problem();
            }
        }

        /// <summary>
        /// checking available drones for loading;
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<DroneAvailableDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("availables")]
        public async Task<ActionResult> GetAvailablesDrones()
        {
            try
            {
                return Ok(await repo.CheckAvailability());
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [ProducesResponseType(typeof(IEnumerable<LoadedMedicationsResDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("{droneId}/batterylevel")]
        public async Task<ActionResult> BatteryLevelByDrone(int droneId)
        {
            try
            {
                return Ok(await repo.BatteryLevel(droneId));
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
