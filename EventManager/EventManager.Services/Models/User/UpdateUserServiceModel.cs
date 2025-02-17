﻿namespace EventManager.Services.Models.User
{
    public class UpdateUserServiceModel
    {
        public required string? UserName { get; set; }

        public required string? Email { get; set; }

        public required string? FirstName { get; set; }

        public required string? LastName { get; set; }
    }
}
