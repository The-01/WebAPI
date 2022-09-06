using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly AppDbContext context;
        public PagesController(AppDbContext context)
        {
            this.context = context;
        }

        //Get /api/pages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPages()
        {
            return await context.Pages.OrderBy(x => x.DisplayOrder).ToListAsync();
        }

        //Get /api/pages/id=?
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPage(int id)
        {
            var page = await context.Pages.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            return page;
        }

        //Put /api/pages/id=?
        [HttpPut("{id}")]
        public async Task<ActionResult<Page>> PutPage(Page page, int id)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            context.Entry(page).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        //Post /api/pages
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            context.Pages.Add(page);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPage), page);
        }

        //Delete /api/pages/id=?
        [HttpDelete("{id}")]
        public async Task<ActionResult<Page>> DeletePage(int id)
        {
            var page = await context.Pages.FindAsync(id);
            context.Pages.Remove(page);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
