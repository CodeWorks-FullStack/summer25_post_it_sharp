namespace post_it_sharp.Controllers;


[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase
{

  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth, PicturesService picturesService, WatchersService watchersService)
  {
    _albumsService = albumsService;
    _auth = auth;
    _picturesService = picturesService;
    _watchersService = watchersService;
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

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesInAlbum(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesInAlbum(albumId);
      return Ok(pictures);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/watchers")]
  public ActionResult<List<WatcherProfile>> GetAlbumWatchers(int albumId)
  {
    try
    {
      List<WatcherProfile> watchers = _watchersService.GetAlbumWatchers(albumId);
      return Ok(watchers);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}