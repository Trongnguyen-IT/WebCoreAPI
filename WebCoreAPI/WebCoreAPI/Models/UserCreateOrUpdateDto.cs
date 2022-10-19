﻿namespace WebCoreAPI.Models
{
    public record UserCreateOrUpdateDto
    {
        public int Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
