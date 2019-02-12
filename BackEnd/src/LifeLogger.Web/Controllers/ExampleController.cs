using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeLogger.Core;
using LifeLogger.Core.Example;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeLogger.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleDbContext _context;
        public ExampleController(ExampleDbContext context)
        {
            _context = context;
            if (_context.ExampleEntities.Count() == 0)
            {
                _context.ExampleEntities.Add(new ExampleEntity
                {
                    ExampleName = "Example Name in DB",
                    ExampleDescription = "Some description",
                    ExampleId = 15
                });

                _context.ExampleEntities.Add(new ExampleEntity
                {
                    ExampleName = "Example Name in DB nr 2",
                    ExampleDescription = "Some description nr 2",
                    ExampleId = 10
                });
                _context.SaveChanges();
            }
        }

        /* To call this endpoint call: https://localhost:44396/api/example */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleEntity>>> GetAllEntities()
        {
            return await _context.ExampleEntities.ToListAsync();
        }

        /* To call this endpoint call: https://localhost:44396/api/example/GetExampleEntityById/15 */
        [HttpGet("GetExampleEntityById/{id}")]
        public async Task<ActionResult<ExampleEntity>> GetExampleEntityById(int id)
        {
            var entity = await _context.ExampleEntities.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        /* To call this endpoint call: https://localhost:44396/api/example/Descripts */
        [HttpGet("Descripts")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllDescriptions()
        {
            return await _context.ExampleEntities
                .Select(x => x.ExampleDescription)
                .ToListAsync();
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ExampleEntity>> Add(ExampleEntity entitiy)
        {
            _context.Add(entitiy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExampleEntityById), new {id = entitiy.ExampleId}, entitiy);
        }
    }
}