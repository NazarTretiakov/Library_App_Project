using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<LibraryDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 3;
            })
            .AddEntityFrameworkStores<LibraryDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, Role, LibraryDbContext, Guid>>()
            .AddRoleStore<RoleStore<Role, LibraryDbContext, Guid>>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/sign-in";
            });

            services.AddAuthorization(options => 
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.AddPolicy("NotAuthorized", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return !context.User.Identity.IsAuthenticated;
                    });
                });
            });

            return services;
        }
    }
}
