﻿namespace Ecommerce.Application.DataTransferObj.User.Request
{
    public class UserUpdateRequest
    {
        //public int UserId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
    }
}
