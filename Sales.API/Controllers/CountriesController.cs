﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries.Include(x => x.States).ToListAsync());
        }

        [HttpGet("full")]
        public async Task<IActionResult> GetFullAsync()
        {
            return Ok(await _context.Countries.Include(x => x.States!).ThenInclude(x => x.Cities).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _context.Countries.Include(x => x.States!).ThenInclude(x => x.Cities).FirstOrDefaultAsync(x => x.Id == id);
            if (country is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);

            }

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            try
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country is null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(country);
                await _context.SaveChangesAsync();
                return NoContent();

            }

        }
    }
}
