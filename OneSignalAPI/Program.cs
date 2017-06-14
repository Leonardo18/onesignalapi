using System;
using System.IO;
using System.Net;
using System.Text;

namespace OneSignalAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic YOR REST API KEY");

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"YOR APP ID\","
                                                    + "\"contents\": {\"en\": \"Notificação via API sem template\"},"
                                                    + "\"small_icon\": \"ic_stat_icone_app_black\","
                                                    + "\"large_icon\": \"ic_stat_icone_app_black\","
                                                    + "\"android_accent_color\": \"BCCF00\","
                                                    //+ "\"big_picture\": \"URL OF IMAGE","                                                    
                                                    + "\"include_player_ids\": [\"DEVICEID\"]}");
                                                    //+ "\"android_background_layout\": {\"image\": \"URL IMAGE", \"headings_color\": \"FFFF0000\", \"contents_color\": \"FF00FF00\"},"
                                                    //+ "\"template_id\": \"737b8a13-22c1-4bdf-83d5-5f92b37b82a5\","
                                                    //+ "\"included_segments\": [\"All\"]}");
            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        Console.Write(responseContent);
                        Console.Read();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
