using Microsoft.AspNetCore.Mvc;

namespace SOAP_Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        // Sample data - replace this with your actual data source
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        [HttpGet]
        [Produces("application/xml")] // Specify XML as the response format
        public IActionResult GetUsers()
        {
            return Ok(_users); // This will automatically serialize the list of users into XML
        }

        [HttpPost]
        [Consumes("application/xml")] // Accept XML in request body
        public IActionResult AddUser([FromBody] User user)
        {
            _users.Add(user);
            return CreatedAtAction(nameof(GetUsers), user); // Return 201 Created status with user data
        }

        [HttpPut("{id}")]
        [Consumes("application/xml")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(); // User with specified id not found
            }

            // Update user properties
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            return NoContent(); // Return 204 No Content status
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(); // User with specified id not found
            }

            _users.Remove(existingUser);
            return NoContent(); // Return 204 No Content status
        }

    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
