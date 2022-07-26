﻿using CRMServer.Models.CRM;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CRMServer.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
