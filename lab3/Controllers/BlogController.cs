using lab3.Models;
using lab3.Models.Entities;
using lab3.Models.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace lab3.Controllers
{
    public class BlogController(BlogContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var entities = await context.BlogArticles.ToListAsync();
            var viewModels = entities.Select(x => x.ToViewModel());
            return View(viewModels);
        }

        public async Task<IActionResult> Article(string id)
        {
            var post = await context.BlogArticles.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = post.ToViewModel();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateBlogArticleModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogArticleModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (await context.BlogArticles.AnyAsync(x => x.Id == viewModel.Id))
            {
                ModelState.AddModelError(nameof(viewModel.Id), "Id must be unique");
                return View(viewModel);
            }
            
            var entity = viewModel.ToEntity();
            context.BlogArticles.Add(entity);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Article), new { id = entity.Id });
        }
    }
}
