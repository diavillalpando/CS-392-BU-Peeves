using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace GMapsAPI
{
    internal class Program
    {
        private static string google_api_key = "";

        static Stream getUrl(string sURL) // makes a get call from the parameter 'sURL' and returns a Stream object
        {
            WebRequest wrGETURL = WebRequest.Create(sURL);
            return wrGETURL.GetResponse().GetResponseStream();
        }

        static string streamSaveImage(Stream stream, string filename) // given a stream and a filename it saves a .PNG to the application folder
        {
            Bitmap bitmap; bitmap = new Bitmap(stream); //reads the stream as a bitmap

            if (bitmap != null)
            {
                bitmap.Save($"{filename}.png"); // if the bitmap isn't empty it saves the image with the given filename
            }
            return AppDomain.CurrentDomain.BaseDirectory + $"{filename}.png"; // returns the filepath for the image
        }

        static string streamToString(Stream stream) // parses a stream into a string
        {
            StreamReader reader = new StreamReader(stream);

            string sLine = reader.ReadLine();
            string returnStr = "";

            while (sLine != null)
            {
                if (sLine != null)
                    returnStr += sLine + "\n";
                sLine = reader.ReadLine();
            }
            return returnStr;
        }


        static string Place_Details(string place_id, string fields = "id,displayName")
        /*
         * place_id - every location in google maps has a place_id associated with it, to find it go to: https://developers.google.com/maps/documentation/javascript/examples/places-placeid-finder
         * fields - the desired fields that are returned (Note: some fields cost money and some don't)
         */
        {
            Stream s = getUrl($"https://places.googleapis.com/v1/places/{place_id}?fields={fields}&key={google_api_key}"); // makes url call
            return streamToString(s); // converts returned stream into a string and returns it
        }
        static string Maps_Photo(string center, string filename = "response", int width = 500, int height = 400, int zoom = 20)
        /*
         * center - "defines the center of the map, equidistant from all edges of the map. This parameter takes a location 
         *          as either a comma-separated {latitude,longitude} pair (e.g. "40.714728,-73.998672") or a string address 
         *          (e.g. "city hall, new york, ny") identifying a unique location on the face of the earth."
         * filename - the name of the PNG saved
         * width - width of image in pixels
         * height - height of image in pixels
         * zoom - "defines the zoom level of the map, which determines the magnification level of the map."
         *          1: World
         *          5: Landmass
         *          10: City
         *          15: 
         *          20: Buildings
         */
        {
            Stream s = getUrl($"https://maps.googleapis.com/maps/api/staticmap?key={google_api_key}&size={width}x{height}&Zoom={zoom}&center=\"{center}\""); // makes url call
            return streamSaveImage(s, filename); // saves stream to the computer
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please paste the Google Maps API key from the .README:");
            google_api_key = Console.ReadLine();

            Console.WriteLine("Details of Boston Univeristy:\n");

            string details = Place_Details("ChIJFzjlY_B544kRyL6j4ABuNCs");  // this is the place_id for boston University

            Console.WriteLine(details + "\n");

            string image_location = Maps_Photo("Boston University, Boston, ma");
            Console.WriteLine($"Photo of map saved to:\n{image_location}");
        }
    }
}
