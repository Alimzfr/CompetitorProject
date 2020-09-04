using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Competitor.Common;
using Competitor.Data;
using Competitor.Domain.Identity;

namespace Competitor.Service.Identity
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISecurityService _securityService;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory,
            ISecurityService securityService)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<CompetitorContext>();
            context.Database.Migrate();
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CompetitorContext>())
                {
                    // Add default roles
                    var adminRole = new Role { Name = CustomRoles.Admin };
                    if (!context.Roles.Any())
                    {
                        context.Add(adminRole);
                        context.SaveChanges();
                    }

                    // Add Admin user
                    if (!context.Users.Any())
                    {
                        var adminUser = new User
                        {
                            Username = "Admin",
                            DisplayName = "Admin",
                            Email = "info@Competitor.ir",
                            IsActive = true,
                            LastLoggedIn = null,
                            Password = _securityService.GetSha256Hash("qwe123QWE!@#"),
                            SerialNumber = Guid.NewGuid().ToString("N")
                        };
                        context.Add(adminUser);
                        context.SaveChanges();

                        context.Add(new UserRole { Role = adminRole, User = adminUser });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}