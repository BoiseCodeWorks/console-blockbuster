using System;
using System.Collections.Generic;

namespace Blockbuster.Models
{
      public enum Genres
    {
        Fantasy = 1,
        Horror,
        Action
    }  
    public class Movie : Item
  {
    public string Title { get; set; }
    public string Plot { get; set; }
    public int ReleaseYear { get; set; }
    public List<Actor> Actors { get; set; } = new List<Actor> ();
    public Genres Genre { get; set; }

    public Movie (string title, string plot, int year, Product product, Genres genre) : base (product)
    {
      Title = title;
      Plot = plot;
      ReleaseYear = year;
      Product = product;
      Genre = genre;
    }
  }
}
