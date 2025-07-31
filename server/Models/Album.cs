
namespace post_it_sharp.Models;

public class Album : DbItem<int>
{
  public string Title { get; set; }
  public string CoverImg { get; set; }
  public string Category { get; set; }
  public string Description { get; set; }
  public bool Archived { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}