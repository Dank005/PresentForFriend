using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Models;
using PresentForFriend.Models.PresentForFriend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PresentForFriend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Present> Presents { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
    }
}
