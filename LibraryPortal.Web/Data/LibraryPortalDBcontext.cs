using LibraryPortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace LibraryPortal.Web.Data
{
    public class LibraryPortalDBcontext: DbContext
    {
        public LibraryPortalDBcontext(DbContextOptions<LibraryPortalDBcontext>options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
