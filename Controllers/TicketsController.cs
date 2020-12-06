using PISLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace PISLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private static List<TicketsData> _memCache = new List<TicketsData>();

        [HttpGet]
        public ActionResult<IEnumerable<TicketsData>> Get()
        {
            return Ok(_memCache);
        }

        [HttpGet("{id}")]
        public ActionResult<TicketsData> Get(int id)
        {
            if (_memCache.Count <= id) return NotFound("No such");
            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TicketsData value)
        {
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            _memCache.Add(value);
            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TicketsData value)
        {
            if (_memCache.Count <= id) return NotFound("No such");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];
            _memCache[id] = value;
            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_memCache.Count <= id) return NotFound("No such");
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }

}
