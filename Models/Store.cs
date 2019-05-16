using System;
using System.Collections.Generic;

namespace Blockbuster.Models
{
  public class Store
  {
    public string Name { get; set; }
    public string Address { get; private set; }
    public Dictionary<Product, List<Item>> Inventory = new Dictionary<Product, List<Item>>();
    public Dictionary<Genres, List<Movie>> Movies { get; set; }

    public void ShowGreeting ()
    {
      System.Console.WriteLine ($@"
Welcome to {Name}
=====================================================================
We currently have {Inventory.Count} product(s) available for rent or purchase

      ");

      foreach (var product in Inventory)
      {
        System.Console.WriteLine ($"{product.Value[0].Name} {product.Value.Count}");

      }
    }

    public void DisplayGenres()
    {
      foreach (KeyValuePair<Genres, List<Movie>> kvp in Movies)
      {
        System.Console.WriteLine($"{kvp.Key}: {kvp.Value.Count}");
      }
      System.Console.WriteLine("Enter a Genre to browse videos:");
      SelectGenre();
    }

    public void SelectGenre()
    {
      try {
      Genres targetGenre;
      while(!Enum.TryParse(Console.ReadLine(), true, out targetGenre))
      {
        System.Console.WriteLine("Not a valid genre.");
      }
      ListMoviesByGenre(targetGenre);
      } catch(KeyNotFoundException err) {
        System.Console.WriteLine(err.Message);
        SelectGenre();
      }
    }

    public void ListMoviesByGenre(Genres genre)
    {
      // declared a placeholder variable
      Dictionary<string, int> availbleMovies = new Dictionary<string, int>();
      // iterate over the genre list and total up the copies of each movie
      if (!Movies.ContainsKey(genre)) {
        System.Console.WriteLine("no movies under that genre available");
        SelectGenre();
      }
      foreach(Movie movie in Movies[genre])
      {
        if (!availbleMovies.ContainsKey(movie.Name))
        {
          availbleMovies[movie.Name] = 0;
        }
        availbleMovies[movie.Name]++;
      }
      //display to the user each keyvaluepair within available movies
      foreach(var kvp in availbleMovies)
      {
        System.Console.WriteLine($"{kvp.Key} - Number of copies available: {kvp.Value}");
      }
    }

    public void AddItem (Item item)
    {
      if (Inventory.ContainsKey (item.Product))
      {
        Inventory[item.Product].Add (item);
        return;
      }
      Inventory.Add (item.Product, new List<Item> () { item });
    }
    public void AddItem(Movie movie)
    {
      if (!Movies.ContainsKey(movie.Genre))
      {
        Movies.Add(movie.Genre, new List<Movie>());
      }
      Movies[movie.Genre].Add(movie);
      AddItem(movie as Item);
    }

    public Store (string name, string address)
    {
      Name = name;
      Address = address;
      Movies = new Dictionary<Genres, List<Movie>>();
    }

  }
}
