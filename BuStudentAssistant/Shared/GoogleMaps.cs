namespace StudySpotYelpNameSpace;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class GoogleMaps
{
    private IHttpClientFactory ClientFactory;
    private HttpClient Http;
    private IJSRuntime JS;

    private IConfiguration Configuration;

    public GoogleMaps(IConfiguration Configuration, IJSRuntime JS, HttpClient Http, IHttpClientFactory ClientFactory){
        this.Configuration = Configuration;
        this.JS = JS;
        this.Http = Http;
        this.ClientFactory = ClientFactory;
    }
    public async Task<Stream> getUrl(string sURL) // makes a get call from the parameter 'sURL' and returns a Stream object
    {
        var client = ClientFactory.CreateClient();
        
        var request = new HttpRequestMessage(HttpMethod.Get, sURL);
        var response = await client.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStreamAsync();
        } 
        throw new HttpRequestException("Response Success Status Code not successful" );
    }

    public string streamToString(Stream stream) // parses a stream into a string
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

    public async Task setImage(Stream imageStream, String img_id)
    {
        // ---- Make sure to copy this into your razor file ----
        // <script> //Script that updates an image 
        // window.setImage = async (imageElementId, imageStream) => {
        //     const arrayBuffer = await imageStream.arrayBuffer();
        //     const blob = new Blob([arrayBuffer]);
        //     const url = URL.createObjectURL(blob);
        //     const image = document.getElementById(imageElementId);
        //     image.onload = () => {
        //     URL.revokeObjectURL(url);
        //     }
        //     image.src = url;
        // }
        // </script>
        
        Console.WriteLine($"Updating '{img_id}' <img>...");
        var dotnetImageStream = new DotNetStreamReference(imageStream);
        await JS.InvokeVoidAsync("setImage", img_id, dotnetImageStream);
        // StateHasChanged();
    }


    public string Place_Details(string place_id, string fields = "id,displayName")
    /*
        * place_id - every location in google maps has a place_id associated with it, to find it go to: https://developers.google.com/maps/documentation/javascript/examples/places-placeid-finder
        * fields - the desired fields that are returned (Note: some fields cost money and some don't)
        */
    {
        Console.WriteLine($"Calling Google Place API for {place_id} details...");
        try{
            
            var api_key = Configuration["GMapsApiKey"];
            var stream = Task.Run(() => getUrl($"https://places.googleapis.com/v1/places/{place_id}?fields=id,displayName&key={api_key}")).Result; // makes url call
            return streamToString(stream); // converts returned stream into a string and returns it
        }catch(Exception ex){
            Console.WriteLine($"Couldn't load place details: {ex.Message}");
            return null;
        }
        
    }

    public async Task Maps_Photo(string center, string photo_id ,int width = 1000, int height = 1000, int zoom = 20)
    {
        Console.WriteLine($"Calling Google Maps API for static image of '{center}' to update '{photo_id}' ");
        try{
            var api_key = Configuration["GMapsApiKey"];
            Stream imageStream = Task.Run(() => getUrl($"https://maps.googleapis.com/maps/api/staticmap?key={api_key}&size={width}x{height}&Zoom={zoom}&center=\"{center}\"")).Result; // makes url call
            await setImage(imageStream,photo_id);
            // StateHasChanged();
        }catch (Exception ex){
            Console.WriteLine($"Couldn't load map photo: {ex.Message}");
        }
        
    }

    public async Task Maps_Photo_Marker(string center, string photo_id ,int width = 1000, int height = 1000, int zoom = 25)
    {
        Console.WriteLine($"Calling Google Maps API for static image of '{center}' to update '{photo_id}' ");
        try{
            var api_key = Configuration["GMapsApiKey"];
            Stream imageStream = Task.Run(() => getUrl($"https://maps.googleapis.com/maps/api/staticmap?key={api_key}&markers=\"{center}\"&size={width}x{height}&Zoom={zoom}&center=\"{center}\"")).Result; // makes url call
            await setImage(imageStream,photo_id);
            // StateHasChanged();
        }catch (Exception ex){
            Console.WriteLine($"Couldn't load map photo: {ex.Message}");
        }
        
    }
}
