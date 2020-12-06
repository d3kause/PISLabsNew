using PISLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PISLabs.Storage;

namespace PISLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private static IStorage<TicketsData> _memCache = new MemCache();
        [HttpGet]
        public ActionResult<IEnumerable<TicketsData>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<TicketsData> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
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
        public IActionResult Put(Guid id, [FromBody] TicketsData value)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];
            _memCache[id] = value;
            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("No such");
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} has been removed");
        }

    }
}


