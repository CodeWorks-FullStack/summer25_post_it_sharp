
namespace post_it_sharp.Models;

public class Picture : DbItem<int>
{
  public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  public Profile Creator { get; set; }
}
