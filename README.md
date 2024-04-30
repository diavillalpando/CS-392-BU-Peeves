# BU Peeves App

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

Install Radzen.Blazor:
https://blazor.radzen.com/docs/guides/getting-started/installation.html

```bash
pip install pymongo
```

Once installed open MongoDBCompass.

Go to Terminal/Command Prompt, find [startServer.py](http://startserver.py/) and run:

```bash
python startServer.py 
```

to connect to the MongoDBCompass.

Connect to the mongodb://localhost:27017. There’s a database called LoginDb and a Users folder. That’s where the user information gets stored.

Populating the server:

```bash
python populateServerStudySpots.py
```
