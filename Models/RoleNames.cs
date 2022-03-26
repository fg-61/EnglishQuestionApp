﻿using System.Collections.Generic;

namespace EnglishQuestionApp.Models
{
        public static class RoleNames
        {
            public static string Admin = "Admin";
            public static string User = "User";

            public static List<string> Roles => new List<string>() { Admin, User };
        }
    } 