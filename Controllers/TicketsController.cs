using PISLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PISLabs.Storage;
using Serilog;

namespace PISLabs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private IStorage<TicketsData> _memCache;

        public TicketsController(IStorage<TicketsData> memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TicketsData>> Get()
        {
            Log.Information("[Tickets Controller] View all object");
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<TicketsData> Get(Guid id)
        {
            if (!_memCache.Has(id))
            {
                Log.Warning($"[Tickets Controller] [No id's] Trying get object with id = {id}");
                return NotFound("No such");
            }
            Log.Information($"[Tickets Controller] View object with id = {id}");
            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TicketsData value)
        {
            var validationResult = value.Validate();
            if (!validationResult.IsValid)
            {
                Log.Error($"[Tickets Controller] [Validation error] Fail post object \"{value.ToString()}\"");
                return BadRequest(validationResult.Errors);
            }
            _memCache.Add(value);
            Log.Information($"[Tickets Controller] Object \"{value.ToString()}\" has ben added");
            return Ok($"{value.ToString()} has been added");
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TicketsData value)
        {
            if (!_memCache.Has(id))
            {
                Log.Error($"[Tickets Controller] [No id's] Fail put object with id = {id}.");
                return NotFound("No such");
            }
            Log.Warning($"[Tickets Controller] Trying update object with id = {id}.");
            var validationResult = value.Validate();
            if (!validationResult.IsValid)
            {
                Log.Error($"[Tickets Controller] [Validation error] Cancel update object with id = {id}.");
                return BadRequest(validationResult.Errors);
            }
            var previousValue = _memCache[id];
            _memCache[id] = value;
            Log.Information($"[Tickets Controller] Object {previousValue.ToString()} has been updated to {value.ToString()}");
            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id))
            {
                Log.Error($"[Tickets Controller] [No id's] Fail removed object with id = {id}.");
                return NotFound("No such");
            }
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            Log.Information($"[Tickets Controller] Object: \"{ valueToRemove.ToString()}\" removed. ");
            return Ok($"{valueToRemove.ToString()} has been removed");
        }

    }
}


