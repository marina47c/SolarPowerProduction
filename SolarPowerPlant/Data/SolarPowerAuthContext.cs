using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Mock;
using SolarPowerAPI.Models.Entities;
using System.Reflection.Emit;

namespace SolarPowerAPI.Data
{
    public class SolarPowerAuthContext: IdentityDbContext
    {
        private readonly RolesMock _rolesMock;

        public SolarPowerAuthContext(DbContextOptions<SolarPowerAuthContext> options, RolesMock rolesMock)
            : base(options)
        {
            _rolesMock = rolesMock;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed data for roles
            List<IdentityRole> roles = _rolesMock.CreateRolesMock();
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
