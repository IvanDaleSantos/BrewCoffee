using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Controllers
{
    [Route("brew-coffee")]
    [ApiController]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly BrewCoffeeService _brewCoffeeService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public BrewCoffeeController(BrewCoffeeService service, IDateTimeProvider provider)
        {
            _brewCoffeeService = service;
            _dateTimeProvider = provider;
        }

        [HttpGet]
        public IActionResult Brew()
        {
            var status = _brewCoffeeService.CheckMachineStatus();

            if(status == CoffeeStatus.Unavailable) return StatusCode(418);
            if(status == CoffeeStatus.NoCoffee) return StatusCode(503);

            return Ok(new
            {
                message = "Your piping hot coffee is ready",
                prepared = _dateTimeProvider.SystemDateTime.ToString("o")
            });
        }
    }
}
