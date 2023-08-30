using AccountService.Common.Settings;

namespace AccountService.Common.Helpers {
    public static class ImageHelper {
        public static string SaveImage(string base64String) {
            string imagePath = @"{0}.png";
            imagePath = string.Format(imagePath, Guid.NewGuid());
            var bytes = Convert.FromBase64String(base64String);
            if (!Directory.Exists($"{AccountApiSettings.ImageRootPath}")) {
                DirectoryHelper.CreateDirectory($"{AccountApiSettings.ImageRootPath}");
            }
            var completePath = $"{AccountApiSettings.ImageRootPath}{imagePath}";
            using var imageFile = new FileStream(completePath, FileMode.Create);
            imageFile.Write(bytes, 0, bytes.Length);
            imageFile.Flush();
            return imagePath;
        }
        public static ByteArrayContent Get(string path) {
            string fullPath = Path.Combine(AccountApiSettings.ImageRootPath, path);
            return new ByteArrayContent(File.ReadAllBytes(fullPath));
        }
        public static void DeleteImage(string imageName) {
            if (File.Exists($"{AccountApiSettings.ImageRootPath}{imageName}")) {
                File.Delete($"{AccountApiSettings.ImageRootPath}{imageName}");
            }
        }
    }
}
