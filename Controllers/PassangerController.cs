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
    public class PassangerController : ControllerBase
    {
        private IStorage<PassangersData> _memCache;

        public PassangerController(IStorage<PassangersData> memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PassangersData>> Get()
        {
            Log.Information("[Passangers controller] View all object");
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<PassangersData> Get(Guid id)
        {
            if (!_memCache.Has(id))
            {
                Log.Warning($"[Passangers controller] [No id's] Trying get object with id = {id}");
                return NotFound("No such");
            }
            Log.Information($"[Passangers controller] View object with id = {id}");
            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PassangersData value)
        {
            var validationResult = value.Validate();
            if (!validationResult.IsValid)
            {
                Log.Error($"[Passangers controller] [Validation error] Fail post object \"{value.ToString()}\"");
                return BadRequest(validationResult.Errors);
            }
            _memCache.Add(value);
            Log.Information($"[Passangers controller] Object \"{value.ToString()}\" has ben added");
            return Ok($"{value.ToString()} has been added");
        }


        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] PassangersData value)
        {
            if (!_memCache.Has(id))
            {
                Log.Error($"[Passangers controller] [No id's] Fail put object with id = {id}.");
                return NotFound("No such");
            }
            Log.Warning($"[Passangers controller] Trying update object with id = {id}.");
            var validationResult = value.Validate();
            if (!validationResult.IsValid)
            {
                Log.Error($"[Passangers controller] [Validation error] Cancel update object with id = {id}.");
                return BadRequest(validationResult.Errors);
            }
            var previousValue = _memCache[id];
            _memCache[id] = value;
            Log.Information($"[Passangers controller] Object {previousValue.ToString()} has been updated to {value.ToString()}");
            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id))
            {
                Log.Error($"[Passangers controller] [No id's] Fail removed object with id = {id}.");
                return NotFound("No such");
            }
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            Log.Information($"[Passangers controller] Object: \"{ valueToRemove.ToString()}\" removed. ");
            return Ok($"{valueToRemove.ToString()} has been removed");
        }

    }
}
    
