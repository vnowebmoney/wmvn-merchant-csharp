using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WM.Merchant.Helpers
{
    public class NetHelper
    {
        /// <summary>
        /// Get Base URI of input uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetBaseURI(Uri uri)
        {
            string baseUrl = uri.Scheme + "://" + uri.Host;
            if (uri.IsDefaultPort)
            {
                return baseUrl;
            }

            return baseUrl + ":" + uri.Port;
        }

        /// <summary>
        /// Get IP address if request client
        /// </summary>
        /// <returns>Remote ip address</returns>
        public static string GetClientIPAddress()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            //test for non-standard proxy server designations of client's IP
            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            //test for host address reported by the server
            else if
            (
                //if exists
                (httpReq.UserHostAddress.Length != 0)
                &&
                //and if not localhost IPV6 or localhost name
                ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
            )
            {
                strIP = httpReq.UserHostAddress;
            }
            //finally, if all else fails, get the IP from a web scrape of another server
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }
                //scrape ip from the html
                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }

        /// <summary>
        /// Send HTTP request to URI, using for API request
        /// </summary>
        /// <param name="URI">Destination URI</param>
        /// <returns>Data response as string</returns>
        public static string HttpRequest(string URI)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(URI);
            // Get the response.
            WebResponse response = request.GetResponse();

            if (response == null)
            {
                return null;
            }
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return responseFromServer.Trim();
        }

        /// <summary>
        /// Send HTTP request to URI, using for API request
        /// </summary>
        /// <param name="URI">Destination URI</param>
        /// <returns>Data response as string</returns>
        public static string HttpRequest(WebRequest request, string requestBody)
        {
            if (request.Method == "GET")
            {
                request.Method = "POST";
            }
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

            // counts content length, and converts requestBody string to byte
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(requestBody);
            int ContentLength = bytes.Length;
            request.ContentLength = ContentLength;
            System.IO.Stream os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            // Get the response.
            WebResponse response = request.GetResponse();

            if (response == null)
            {
                return null;
            }
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return responseFromServer.Trim();
        }
    }
}
