using System;
using System.Net;
using EDPortTest.Port;
using Newtonsoft.Json;

namespace EDPortTest.Web
{
    internal class RequestBody
    {
        public string IP;
        public int Port;
        private RequestBody()
        {

        }
    }
    internal class HTTP
    {
        /// <summary>
        /// Runs TestPort and returns a JSON result, encoded in UFT8 bytes
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <param name="body">HTTP Post Body</param>
        /// <returns>UTF-8 encoded JSON reponse</returns>
        private static byte[] CreateResponse(HttpListenerRequest request, string body)
        {
            if (request.HttpMethod != "POST")
            {// http handling
                return System.Text.Encoding.UTF8.GetBytes("{"
                + $"'error': True,"
                + $"'message': 'Expected POST.'"
                + "}\n");
            }
            try
            {
                var json = JsonConvert.DeserializeObject<RequestBody>(body);

                bool res = PortTester.TestPort(json!.IP, json!.Port);

                return System.Text.Encoding.UTF8.GetBytes("{"
                + $"'error': False,"
                + $"'message': '{(res ? "OK" : "NOTOK")}'"
                + "}\n");
            }
            catch (Exception e)
            {
                return System.Text.Encoding.UTF8.GetBytes("{"
                + $"'error': True,"
                + $"'message': '{e.Message}'"
                + "}\n");
            }
        }

        /// <summary>
        /// Handles HTTP requests in a seperate thread
        /// </summary>
        /// <param name="context">HTTP Request Context</param>
        /// <returns></returns>
        private static Task<int> RequestHandler(HttpListenerContext context)
        {
            var request = context.Request;
            string body = "";
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                body = reader.ReadToEnd();
            }

            var response = context.Response;
            byte[] buffer = CreateResponse(request, body);
            response.ContentLength64 = buffer.Length;
            response.Headers.Add("Access-Control-Allow-Origin", "https://niceygy.net");
            response.Headers.Add("Content-Type", "application/json");
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            return Task.FromResult(0);
        }

        /// <summary>
        /// Starts the HTTP server, and delegates to RequestHandler to handle responses
        /// </summary>
        /// <param name="address">IP Address. e.g localhost</param>
        /// <param name="port">TCP Port</param>
        public static void Serve(string address, int port)
        {
            HttpListener listener = new();
            listener.Prefixes.Add($"http://{address}:{port}/");
            listener.Start();
            Console.WriteLine($"Listening on http://{address}:{port}/ ...");

            while (true)
            {
                var context = listener.GetContext();
                Task.Run(() => RequestHandler(context));
            }
        }
    }
}