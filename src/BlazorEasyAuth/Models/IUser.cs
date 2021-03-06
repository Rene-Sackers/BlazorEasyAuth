using System;

namespace BlazorEasyAuth.Models
{
    public interface IUser
    {
        string GetId();
        
        string DisplayName { get; set; }
        
        string Username { get; set; }
        
        byte[] PasswordHash { get; set; }
        
        string[] Roles { get; set; }
        
        bool IsDeleted { get; set; }
        
        DateTimeOffset PermissionsChangeDate { get; set; }
    }
}