﻿using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.Domain.RepositoryContracts;
using LibraryApp.Core.ServiceContracts;
using LibraryApp.Core.Services;
using LibraryApp.Infrastructure.DbContext;
using LibraryApp.Infrastructure.Repositories;
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


            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ISavesRepository, SavesRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();

            services.AddScoped<IUsersGetterService, UsersGetterService>();
            services.AddScoped<IPostsCreatorService, PostsCreatorService>();
            services.AddScoped<IPostsGetterService, PostsGetterService>();
            services.AddScoped<ILikesGetterService, LikesGetterService>();
            services.AddScoped<ILikesAdderService, LikesAdderService>();
            services.AddScoped<ILikesRemoverService, LikesRemoverService>();
            services.AddScoped<IToggleLikeService, ToggleLikeService>();
            services.AddScoped<IIsPostLikedService, IsPostLikedService>();
            services.AddScoped<ISavesGetterService, SavesGetterService>();
            services.AddScoped<ISavesAdderService, SavesAdderService>();
            services.AddScoped<ISavesRemoverService, SavesRemoverService>();
            services.AddScoped<IToggleSaveService, ToggleSaveService>();
            services.AddScoped<IIsPostSavedService, IsPostSavedService>();
            services.AddScoped<IIsBookSavedService, IsBookSavedService>();
            services.AddScoped<ICommentsGetterService, CommentsGetterService>();
            services.AddScoped<ICommentsCreatorService, CommentsCreatorService>();
            services.AddScoped<ISubscriptionsGetterService, SubscriptionsGetterService>();
            services.AddScoped<ISubscriptionsAdderService, SubscriptionsAdderService>();
            services.AddScoped<ISubscriptionsRemoverService, SubscriptionsRemoverService>();
            services.AddScoped<IToggleSubscriptionService, ToggleSubscriptionService>();
            services.AddScoped<IIsCurrentWorkingUserSubscribedService, IsCurrentWorkingUserSubscribedService>();
            services.AddScoped<IChangeProfileInformationService, ChangeProfileInformationService>();
            services.AddScoped<IChangeProfilePhotoService, ChangeProfilePhotoService>();
            services.AddScoped<IAuthorsGetterService, AuthorsGetterService>();
            services.AddScoped<IAuthorsAdderService, AuthorsAdderService>();
            services.AddScoped<IAuthorsRemoverService, AuthorsRemoverService>();
            services.AddScoped<IBooksAdderService, BooksAdderService>();
            services.AddScoped<IBooksGetterService, BooksGetterService>();
            services.AddScoped<IChangeBookAmountService, ChangeBookAmountService>();
            services.AddScoped<IBooksRemoverService, BooksRemoverService>();
            services.AddScoped<IReviewsCreatorService, ReviewsCreatorService>();
            services.AddScoped<IReviewsGetterService, ReviewsGetterService>();
            services.AddScoped<IOrdersCreatorService, OrdersCreatorService>();
            services.AddScoped<IOrdersGetterService, OrdersGetterService>();
            services.AddScoped<IOrderStatusChangerService, OrderStatusChangerService>();
            services.AddScoped<INotificationsCreatorService, NotificationsCreatorService>();
            services.AddScoped<INotificationsGetterService, NotificationsGetterService>();
            services.AddScoped<IMessagesCreatorService, MessagesCreatorService>();
            services.AddScoped<IMessagesGetterService, MessagesGetterService>();


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
