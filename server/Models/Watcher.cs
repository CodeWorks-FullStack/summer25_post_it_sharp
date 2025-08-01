namespace post_it_sharp.Models;

public class Watcher : DbItem<int>
{
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
  // NOTE nesting data into our many to many does not make sense when we have so much control over our data from the data base
  // public Profile profile { get; set; } 
  // public Album Album { get; set; }
}

public class WatcherProfile : Profile
{
  public int WatcherId { get; set; }
  public int AlbumId { get; set; }
}

public class WatcherAlbum : Album
{
  public int WatcherId { get; set; }
  public string AccountId { get; set; }
}