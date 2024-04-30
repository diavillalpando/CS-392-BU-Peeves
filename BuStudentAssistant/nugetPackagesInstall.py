import os

os.system("clear")
print("---- Adding NuGet Packages ----")

print("---MongoDb Driver...")
os.system("dotnet add package MongoDB.Driver")

print("---Radzen...")
os.system("dotnet add package Radzen.Blazor")

print("---Http Extension...")
os.system("dotnet add package Microsoft.Extensions.Http")
