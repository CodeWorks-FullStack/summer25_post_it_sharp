namespace post_it_sharp.Models;


public class Profile : DbItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }
}