using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hazel
{
    public class HttpRequest
    {
        public static String OpenUrl(String url)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static void OpenUrl(String url, ref MemoryStream memory)
        {
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (BufferedStream reader = new BufferedStream(response.GetResponseStream()))
                {
                    byte[] buffer = new byte[1024 * 1024 * 10];
                    int currentLength = 0;
                    int readLength;
                    while ((readLength = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        currentLength += readLength;
                        Debug.WriteLine(currentLength);
                        memory.Write(buffer, 0, readLength);
                    }
                }
            }
        }
    }
}
