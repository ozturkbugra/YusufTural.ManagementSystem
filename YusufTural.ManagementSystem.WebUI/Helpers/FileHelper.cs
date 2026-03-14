namespace YusufTural.ManagementSystem.WebUI.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> UploadFile(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0) return null;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            // Dosyaları wwwroot/uploads altına atıyoruz
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", subFolder);

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{subFolder}/{fileName}";
        }

        public static void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
