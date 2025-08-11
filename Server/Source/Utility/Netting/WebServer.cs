using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMethylamine.Source.Utility.Netting
{
    public class WebServer
    {
        private readonly FileServer _fileServer;
        private readonly HttpServer _httpServer;

        public WebServer(string domain, int httpsPort, string staticFileRoot)
        {
            _fileServer = new FileServer(staticFileRoot);

            string[] prefixes = new string[]
            {
                $"https://{domain}:{httpsPort}/",
                $"http://localhost:8080/"
            };

            _httpServer = new HttpServer(prefixes, HandleRequestAsync);
        }

        public Task StartAsync() => _httpServer.StartAsync();

        public void Stop() => _httpServer.Stop();

        private async Task HandleRequestAsync(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // CORS
            response.AddHeader("Access-Control-Allow-Origin", "*");
            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");
            response.AddHeader("Access-Control-Max-Age", "86400");

            if (request.HttpMethod == "OPTIONS")
            {
                response.StatusCode = 200;
                response.Close();
                return;
            }

            Console.WriteLine($"[INFO] {request.HttpMethod} {request.Url.AbsolutePath} from {request.RemoteEndPoint}");

            switch (request.Url.AbsolutePath.ToLower())
            {
                case "/":
                case "/status":
                    await WriteResponseAsync(response, "Project Methylamine Server Online", "text/plain");
                    return;

                case "/crossdomain.xml":
                    await WriteResponseAsync(response, GenerateCrossDomainXml(), "text/xml");
                    return;

                case "/api/test":
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        string body = await reader.ReadToEndAsync();
                        Console.WriteLine($"[INFO] API POST Body: {body}");
                    }

                    await WriteResponseAsync(response, "{\"status\":\"success\",\"message\":\"API endpoint working\"}", "application/json");
                    return;

                default:
                    await _fileServer.ServeFileAsync(context);
                    return;
            }
        }

        private static async Task WriteResponseAsync(HttpListenerResponse response, string content, string contentType)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentType = contentType;
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        private static string GenerateCrossDomainXml()
        {
            return @"<?xml version=""1.0""?>
<!DOCTYPE cross-domain-policy SYSTEM ""http://www.adobe.com/xml/dtds/cross-domain-policy.dtd"">
<cross-domain-policy>
    <site-control permitted-cross-domain-policies=""master-only""/>
    <allow-access-from domain=""*"" />
    <allow-http-request-headers-from domain=""*"" headers=""*"" secure=""false""/>
</cross-domain-policy>";
        }
    }
}
