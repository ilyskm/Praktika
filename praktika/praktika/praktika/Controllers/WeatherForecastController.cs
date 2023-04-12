using Microsoft.AspNetCore.Mvc;

namespace praktika.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            for (int i = 0; i < Summaries.Count; i++)
            {
                if (string.Equals(name, Summaries[i])) { break; }
                else if (i + 1 == Summaries.Count && !string.Equals(name, Summaries[i]))
                {
                    Summaries.Add(name);
                }
            }
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if(index<0)
            {
                return BadRequest("Индекс не может быть отрицательным!");
            }
            if (index>= Summaries.Count) 
            {
                return BadRequest("Индекс не существует!");            
            }
            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 )
            {
                return BadRequest("Такой индекс неверный!");
            }
            if (index>=Summaries.Count)
            {
                return BadRequest("Индекса не существует!");
            }
            else
            {
                for (int i = 0; i < Summaries.Count; i++)
                {
                    if (string.Equals(name, Summaries[i])) { break; }
                    else if (i + 1 == Summaries.Count && !string.Equals(name, Summaries[i]))
                    {
                        Summaries[index] = name;
                    }
                }
                return Ok();
            }
        }
        [HttpGet("{index}")]
        public string GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return ("Invalid index!");
            }
            return Summaries[index];
        }
        [HttpGet]
        public List<string> GetAll(int? sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Summaries;
            }
            else if (sortStrategy == 1)
            {
                Summaries.Sort();
                return Summaries;
            }
            else if (sortStrategy == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();
                return Summaries;
            }
            return Summaries;
        }
        [HttpGet("find-by-name")]
        public int GetByName(string name)
        {
            int a = 0;
            for (int i = 0; i < Summaries.Count; i++)
            {
                if (Summaries[i] == name)
                    a++;
            }
            return a;
        }
    }
}