﻿using LibraryApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Core.Domain.IdentityEntities
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(20)]
        public string? Firstname { get; set; }

        [StringLength(30)]
        public string? Lastname { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(30)]
        public string? ProfilePhotoPath { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public bool IsBlocked { get; set; }

        public List<Post>? Posts { get; set; }
        public List<Like>? Likes { get; set; }
        public List<Save>? Saves { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Subscription>? Subscribers { get; set; }
        public List<Subscription>? Subscriptions { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
