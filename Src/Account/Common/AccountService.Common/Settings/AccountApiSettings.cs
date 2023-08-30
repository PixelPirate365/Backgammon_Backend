using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Settings {
    public static class AccountApiSettings {
        public static string ApiBaseUrl { get; set; } = null!;
        public static string ImageRootPath { get; set; } = null!;
    }
}
