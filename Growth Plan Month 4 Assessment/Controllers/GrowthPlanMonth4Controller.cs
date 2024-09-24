using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;


namespace Growth_Plan_Month_4_Assessment.Controllers
{
    [ApiController]
    [Route("jsonpatch/[action]")]
    public class GrowthPlanMonth4Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private books oneBook = new books(1, "asif", "Chemistry", 23, "Screenshot (4).png");
        private List<books> books = new List<books> {new books(1, "asif", "Chemistry", 23, "Screenshot (4).png"),
        new books(2, "test1", "English", 24, "Screenshot (1).png"),
        new books(3, "test2", "English", 24, "Screenshot (2).png")
        };

        private readonly ILogger<GrowthPlanMonth4Controller> _logger;

        public GrowthPlanMonth4Controller(ILogger<GrowthPlanMonth4Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProductPrice([FromBody]  JsonPatchDocument<books> newbook )
        {
            newbook.ApplyTo(oneBook, ModelState);

           
            return Ok(oneBook);
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(books);
        }

        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            return Ok(book);
        }
        [HttpGet]
        public IActionResult GetAllImages()
        {
            var images=books.Select(x=> x.ImageUrl).ToList();
            return Ok(images);
        }
        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var image = books.FirstOrDefault(books=> books.Id == id);
            return Ok(image);
        }

    }
}