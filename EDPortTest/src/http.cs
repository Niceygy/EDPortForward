using System;
using System.Net;
using EDPortTest.Port;

namespace EDPortTest.Web
{


    class HTTP
    {
        private static byte[] CreateResponse(HttpListenerRequest request, System.Collections.Specialized.NameValueCollection headers)
        {

            request.
            bool res = PortTester();


        }

        public static void Serve(string address, int port)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://{address}:{port}/");
            listener.Start();
            Console.WriteLine($"Listening on http://{address}:{port}/ ...");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var headers = request.Headers;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string body = reader.ReadToEnd();
                }

                var response = context.Response;
                // string responseString = "<html><body>Hello, world!</body></html>";
                // byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                byte[] buffer = CreateResponse(request, headers);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }
    }
}