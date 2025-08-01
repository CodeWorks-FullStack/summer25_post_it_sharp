namespace post_it_sharp.Controllers;

[ApiController]
[Route("api/pictures")]
public class PicturesController : ControllerBase
{

  private readonly PicturesService _picturesService;
  private readonly Auth0Provider _auth;

  public PicturesController(Auth0Provider auth, PicturesService picturesService)
  {
    _auth = auth;
    _picturesService = picturesService;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      pictureData.CreatorId = userInfo.Id;
      Picture picture = _picturesService.CreatePicture(pictureData);
      return Ok(picture);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}