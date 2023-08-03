using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPPEF.Data;
using TestWebAPPEF.Models;
using TestWebAPPEF.Services;

namespace TestWebAPPEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly TestEFContext _context;
        private readonly IMovieInterface _movieInterface;

        public MoviesController(TestEFContext context, IMovieInterface movieInterface)
        {
            _context = context;
            _movieInterface = movieInterface;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetMovie()
        {
          if (_context.Movie == null)
          {
                return default;
          }
            //return await _context.Movie.ToListAsync();

            //return Ok(_context.Movie.Adapt<List<MovieDto>>());
            var movie =await _movieInterface.GetMovie();
            return movie;
        }

        // GET: api/Movies/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Movie>> GetMovie(int id)
        //{
        //  if (_context.Movie == null)
        //  {
        //      return NotFound();
        //  }
        //    var movie = await _context.Movie.FindAsync(id);

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return movie;
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            var movie = _context.Movie.Find(id).Adapt<MovieDto>();
            //var movies = new MovieDto();
            //_context.Movie.Find(id).Adapt(movies);

            //var movie = _context.Movie.ProjectToType<MovieDto>().First(i => i.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
           

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }


            var movie1=await _movieInterface.PutMovie(id, movie);

            if(movie1 == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //public async Task<IActionResult> PutMovie(int id, MovieDto movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return BadRequest();
        //    }
        //    var movies = movie.Adapt<Movie>();
        //    _context.Entry(movies).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        //{
        //  if (_context.Movie == null)
        //  {
        //      return Problem("Entity set 'TestEFContext.Movie'  is null.");
        //  }
        //    _context.Movie.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        //}

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDto movie)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'TestEFContext.Movie'  is null.");
            }

            var movie1 =await _movieInterface.PostMovie(movie);

            return CreatedAtAction("GetMovie", new { id = movie1.Id }, movie1);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_movieInterface.DeleteMovie == null)
            {
                return NotFound();
            }
            var movie = await _movieInterface.DeleteMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool MovieExists(int id)
        //{
        //    return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
