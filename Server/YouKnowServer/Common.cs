using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace YouKnowServer
{
    public class Common
    {
        public static string Push(double latitude, double longitude, double radius, string messageToDisplay,Guid type,int typeId)
        {
            string apikey = "632e82a3-3ff7-49b5-8b65-2005eb3bbb7e";
            string authorization = "MDY1Mzg3NzYtMzhhNC00ZjVjLWJjMjEtZTI5MWYzZGY2OTli";
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Basic " + authorization);
            var payload = "{"
                          + "\"app_id\": \"" + apikey + "\","
                          + "\"included_segments\": \"All\","
                          + "\"ios_badgeType\": \"Increase\","
                          + "\"ios_badgeCount\": 1,"
                          + "\"data\": { \"TypeId\": \"" + type + "\" ,  \"Type\": \"" + typeId +  "\" },"
                          + "\"contents\": { \"en\": \"" + messageToDisplay + "\"},"
                          + "\"headings\": { \"en\": \"YouKnow \"},"
                          + "\"location\" : [{\"radius \": \"" + radius + "\", \"lat\": \""+ latitude + "\", \"long \": \""+ longitude +"\"}]}";
            byte[] byteArray = Encoding.UTF8.GetBytes(payload);
            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }
                string content = "";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                        //dynamic item = serializer.Deserialize<object>(responseContent.ToString());
                        //count = item["recipients"];
                        content = responseContent.ToString();
                    }
                }
                return content;
            }
            catch (WebException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return ex.ToString();
            }
        }
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            try
            {
                var text = string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                         "POINT({0} {1})", longitude, latitude);
                // 4326 is most common coordinate system used by GPS/Maps
                return DbGeography.PointFromText(text, 4326);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }


}