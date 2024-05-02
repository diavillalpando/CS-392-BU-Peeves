# BU Student Assistant

#### Names:
- Dia Villalpando
- Ana Di Tano
- Karla Vazquez
- SongYu Chen
- Addison Baum

Packages:

Top-level Packages:                                             

> AspNetCore.Identity.Mongo
> 
> 
> AspNetCore.Identity.MongoDbCore                                
> 
> BCrypt.Net-Next                                               
> 
> Betalgo.OpenAI                                              
> 
> Betalgo.OpenAI.GPT3                                         
> 
> Blazored.SessionStorage                                    
> 
> Google.Apis.Gmail.v1                                   
> Microsoft.AspNetCore.Authentication.Google              
> 
> [Microsoft.AspNetCore.Components.Web](http://microsoft.aspnetcore.components.web/)                    
> 
> Microsoft.AspNetCore.Components.WebAssembly.Authentication      
> 
> Microsoft.AspNetCore.Identity.EntityFrameworkCore           
> 
> Microsoft.AspNetCore.Identity.UI        
> 
> Microsoft.AspNetCore.Session                     
> 
> Microsoft.EntityFrameworkCore.Sqlite                  
> 
> Microsoft.EntityFrameworkCore.Tools             
> 
> MimeKit                                                 
> 
> MongoDB.Driver                                       
> 
> Newtonsoft.Json                                             
> 
> Quick.AspNetCore.Components.Web.Extensions        
> 
> Radzen.Blazor
> 

## Installations:

Install .NET:
https://dotnet.microsoft.com/en-us/download

Install MongoDb (Choose your OS):
https://www.mongodb.com/try/download/community-kubernetes-operator 

Install pymongo:
https://pypi.org/project/pymongo/

```bash
pip install pymongo
```

#### For the following CD into the BuStudentAssistant folder:

Drag "appsettings.json" and "appsettings.Development.json" into the folder

Go to Terminal/Command Prompt, find [startServer.py](http://startserver.py/) and run to start the mongoDB server:

```bash
python startServer.py 
```

Open MongoDBCompass.

to connect to the MongoDBCompass.

Connect to the mongodb://localhost:27017. There’s a database called LoginDb. That’s where information gets stored.

#### Populating the server:

```bash
python populateServerStudySpots.py
```

#### To start the app:
- From windows: run the app on visual studio
- From mac: on the BuStudentAssistant folder:
  ```bash
  dotnet watch
  ```
