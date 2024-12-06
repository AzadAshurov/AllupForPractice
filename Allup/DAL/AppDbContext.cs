﻿using Allup.Models;
using Microsoft.EntityFrameworkCore;

namespace Allup.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slide> Slides { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
