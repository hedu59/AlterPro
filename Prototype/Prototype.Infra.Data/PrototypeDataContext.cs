

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Prototype.Domain.Entities;
using Prototype.Infra.Data.Interfaces;
using System;
using System.Linq;

namespace Prototype.Infra.Data
{
    public class PrototypeDataContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public PrototypeDataContext(DbContextOptions<PrototypeDataContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;             
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Invitation> Invitites { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));//connection string get by appsetting.json
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var mappings = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => typeof(IEntityMapping).IsAssignableFrom(x) && !x.IsAbstract)
                    .ToList();

            foreach (var type in mappings)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
       
    }
}
