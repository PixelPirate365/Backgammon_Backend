using AccountService.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Helpers {
    public static class DirectoryHelper {
        public static DirectoryInfo CreateDirectory(string dirName) {
            var result = Directory.CreateDirectory(Path.Combine(AccountApiSettings.ImageRootPath, dirName));
            return result;
        }
    }
}
