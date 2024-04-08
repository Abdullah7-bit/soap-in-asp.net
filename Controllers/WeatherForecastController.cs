using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOAP_Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOAP_Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DataContext _dbContext;

        private static readonly string[] Summaries = new[]
        {
            "freezing", "bracing", "chilly", "cool", "mild", "warm", "balmy", "hot", "sweltering", "scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // Constructor injection for ILogger
        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("weather")] // Unique route for this action
        [Produces("application/json")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            _logger.LogInformation("Getting weather forecast data...");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = new Random().Next(-20, 55),
                Summary = Summaries[new Random().Next(Summaries.Length)]
            }).ToArray();
        }

        [HttpGet]
        [Produces("application/xml")]
        public IActionResult GetBooks()
        {
            try
            {
                var books = _dbContext.Books.ToList(); // Retrieve all books from the database

                return Ok(books); // Return books in XML format
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // Sample data - replace this with your actual data source
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };

        [HttpGet("users")] // Unique route for this action
        [Produces("application/xml")]
        public IActionResult GetUsers()
        {
            _logger.LogInformation("Getting users...");

            return Ok(_users);
        }

        [HttpPost("users")] // Unique route for this action
        [Consumes("application/xml")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _users.Add(user);

            _logger.LogInformation($"Added user with ID: {user.Id}");

            return CreatedAtAction(nameof(GetUsers), user);
        }

        [HttpPut("users/{id}")] // Unique route for this action
        [Consumes("application/xml")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;

            _logger.LogInformation($"Updated user with ID: {id}");

            return NoContent();
        }

        [HttpDelete("users/{id}")] // Unique route for this action
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _users.Find(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _users.Remove(existingUser);

            _logger.LogInformation($"Deleted user with ID: {id}");

            return NoContent();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
