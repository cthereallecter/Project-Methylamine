using System;
using System.Net;

namespace ProjectMethylamine.Source.Utility.Netting
{
    public class FileServer
    {
        private readonly string _baseDirectory;

        public FileServer(string baseDirectory)
        {
            if (!Directory.Exists(baseDirectory))
                throw new DirectoryNotFoundException($"Directory not found: {baseDirectory}");

            _baseDirectory = baseDirectory;
        }

        public async Task ServeFileAsync(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            string relativePath = request.Url.AbsolutePath.TrimStart('/');
            if (string.IsNullOrEmpty(relativePath))
                relativePath = "index.html"; // Default document

            string filePath = Path.Combine(_baseDirectory, relativePath);

            if (!File.Exists(filePath))
            {
                response.StatusCode = 404;
                byte[] notFound = System.Text.Encoding.UTF8.GetBytes("404 - File Not Found");
                await response.OutputStream.WriteAsync(notFound, 0, notFound.Length);
                response.OutputStream.Close();
                return;
            }

            try
            {
                // Detect MIME type (simplified)
                string contentType = GetMimeType(Path.GetExtension(filePath));
                response.ContentType = contentType;

                byte[] buffer = await File.ReadAllBytesAsync(filePath);
                response.ContentLength64 = buffer.Length;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                byte[] error = System.Text.Encoding.UTF8.GetBytes($"500 - Internal Server Error: {ex.Message}");
                await response.OutputStream.WriteAsync(error, 0, error.Length);
                response.OutputStream.Close();
            }
        }

        private static string GetMimeType(string extension)
        {
            return extension.ToLower() switch
            {
                ".html" => "text/html",
                ".htm" => "text/html",
                ".js" => "application/javascript",
                ".css" => "text/css",
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".php" => "application/x-httpd-php",
                ".json" => "application/json",
                _ => "application/octet-stream",
            };
        }
    }
}
