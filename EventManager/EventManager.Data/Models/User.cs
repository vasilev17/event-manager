﻿using EventManager.Common.Constants;
using EventManager.Data.Models.Picture;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace EventManager.Data.Models
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ProfilePicture ProfilePicture { get; set; } = new() { Url = PictureConstants.DefaultUserPicture };

        public HashSet<Role> Roles { get; set; } = new();

        [JsonIgnore]
        public HashSet<Event> Events { get; set; } = new();

        public List<Rating> EventRatings { get; set; } = new();

        public HashSet<Event> AttendedEvents { get; set; } = new();

        public List<Ticket> BookedTickets { get; set; } = new();

    }
}
