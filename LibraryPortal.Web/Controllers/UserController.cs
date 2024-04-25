using LibraryPortal.Web.Data;
using LibraryPortal.Web.Models;
using LibraryPortal.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryPortal.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly LibraryPortalDBcontext dbContext;

        public UserController(LibraryPortalDBcontext dbContext)
        {
            this.dbContext=dbContext;
        }
        [HttpGet]
        public IActionResult BorrowBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(AddBooks viewModel)
        {
            var user = new User
            {
                Author = viewModel.Author,
                Title = viewModel.Title,
                Name = viewModel.Name,
                PhoneNo = viewModel.PhoneNo,
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var user = await dbContext.Users.ToListAsync();
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User viewModel)
        {
            var user = await dbContext.Users.FindAsync(viewModel.Id);

            if(user is not null)
            {
                user.Author = viewModel.Author; 
                user.Title = viewModel.Title;
                user.Name = viewModel.Name;
                user.PhoneNo = viewModel.PhoneNo;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List","User");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(User viewModel)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if(user is not null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "User");
        }

    }
}
