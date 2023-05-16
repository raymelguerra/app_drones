using AppDrones.Core.Dto;
using AppDrones.Core.Extensions;
using AppDrones.Core.Interfaces;
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
        public DispatchController(IDrone drone, IValidator<RegistryReqDto> _registryValidator)
        {
            repo = drone;
            registryValidator = _registryValidator;
        }
        /// <summary>
        /// registering a drone
        /// </summary>
        /// <param name="rates">Json type of ExchangeRate</param>
        /// <response code="201">Returns the exchange rate created (When the system considers today as the future, the entered exchange rate is only saved if it contains all the currencies classified in the system variables, otherwise it fills in the missing ones with the rates of previous days).</response>
        /// <response code="400">Invalid exchange rate input parameter.</response>
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
    }
}
