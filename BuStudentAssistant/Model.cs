using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PlacesContext : DbContext
{
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Place> Places { get; set; }

    public string DbPath { get; }

    public PlacesContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "places.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Place
{
    [Key] public string PlaceName { get; set; }
    public List<Review> Reviews { get; } = new();
}

public class Review
{
    [Key] public int ReviewId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}