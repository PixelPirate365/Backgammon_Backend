using AccountService.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Helpers {
    public static class ImageHelper {
        public static string SaveImage(string base64String) {
            string folder = "/images/profilepictures";
            string imagePath = @"{0}/{1}.png";
            imagePath = string.Format(imagePath, folder, Guid.NewGuid());
            var bytes = Convert.FromBase64String(base64String);
            if (!Directory.Exists($"{AccountApiSettings.ImageRootPath}{folder}")) {
                DirectoryHelper.CreateDirectory($"{AccountApiSettings.ImageRootPath}{folder}");
            }
            var completePath = $"{AccountApiSettings.ImageRootPath}{imagePath}";
            using var imageFile = new FileStream(completePath, FileMode.Create);
            imageFile.Write(bytes, 0, bytes.Length);
            imageFile.Flush();
            return imagePath;
        }
        public static ByteArrayContent Get(string path) {
            string folder = "/images/profilepictures";
            string fullPath = Path.Combine(AccountApiSettings.ImageRootPath, folder, path);
            return new ByteArrayContent(File.ReadAllBytes(fullPath));
        }
        public static void DeleteImage(string imageName) {
            if (File.Exists($"{AccountApiSettings.ImageRootPath}{imageName}")) {
                File.Delete($"{AccountApiSettings.ImageRootPath}{imageName}");
            }
        }
    }
}
