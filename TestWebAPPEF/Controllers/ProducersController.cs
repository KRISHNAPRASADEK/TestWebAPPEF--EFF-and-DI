﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPPEF.Data;
using TestWebAPPEF.Models;

namespace TestWebAPPEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly TestEFContext _context;

        public ProducersController(TestEFContext context)
        {
            _context = context;
        }

        // GET: api/Producers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producer>>> GetProducer()
        {
          if (_context.Producer == null)
          {
              return NotFound();
          }
            return await _context.Producer.ToListAsync();
        }

        // GET: api/Producers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producer>> GetProducer(int id)
        {
          if (_context.Producer == null)
          {
              return NotFound();
          }
            var producer = await _context.Producer.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // PUT: api/Producers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(int id, Producer producer)
        {
            if (id != producer.Id)
            {
                return BadRequest();
            }

            _context.Entry(producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Producers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(Producer producer)
        {
          if (_context.Producer == null)
          {
              return Problem("Entity set 'TestEFContext.Producer'  is null.");
          }
            _context.Producer.Add(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducer", new { id = producer.Id }, producer);
        }

        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(int id)
        {
            if (_context.Producer == null)
            {
                return NotFound();
            }
            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducerExists(int id)
        {
            return (_context.Producer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
