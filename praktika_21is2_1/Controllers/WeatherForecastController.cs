using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace praktika_21is2_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
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
            Summaries.RemoveAt(index);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!");
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
        public IActionResult OutPut(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Invalid index!");
            }
            else
            {
                string res = Summaries[index];
                return Ok(res);

            }
        }
        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            List<string> res = Summaries;
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }
            else if (sortStrategy == 1)
            {
                res.Sort();
                res.Reverse();
                return Ok(res);
            }
            else if (sortStrategy == -1)
            {
                res.Sort();
                return Ok(res);
            }
            else
                return BadRequest("Invalid parametr value");
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
