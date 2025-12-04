using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MyrtilleRDPManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanı Tablolarımız
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<UserConnectionPermission> UserConnectionPermissions { get; set; }
    }
}