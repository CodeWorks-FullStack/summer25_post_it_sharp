namespace post_it_sharp.Controllers;


[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase
{

  private readonly AlbumsService _albumsService;
  private readonly Auth0Provider _auth;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth)
  {
    _albumsService = albumsService;
    _auth = auth;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }

  [HttpGet]
  public ActionResult<List<Album>> GetAllAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAllAlbums();
      return Ok(albums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetOneAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetOneAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{albumId}")]
  [Authorize]
  public async Task<ActionResult<Album>> ArchiveAlbumById(int albumId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbumById(albumId, userInfo.Id);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}