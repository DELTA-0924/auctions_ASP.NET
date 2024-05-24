﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Repository
{
    public  class AuthorizationOptions
    {
        public RolePermissions[] RolePermissions { get; set; } = [];
    }
    public class RolePermissions
    {
        public string Role { get; set; } = string.Empty;
        public string[] Permissions { get; set; } = [];
    }
}