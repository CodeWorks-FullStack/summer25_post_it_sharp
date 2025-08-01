namespace post_it_sharp.Controllers;


[ApiController]
[Route("api/watchers")]
public class WatchersController : ControllerBase
{
  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth;

  public WatchersController(Auth0Provider auth, WatchersService watchersService)
  {
    _auth = auth;
    _watchersService = watchersService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Watcher>> CreateWatcher([FromBody] Watcher watcherData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      watcherData.AccountId = userInfo.Id;
      Watcher watcher = _watchersService.CreateWatcher(watcherData);
      return Ok(watcher);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{watcherId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteWatcher(int watcherId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string deleteMsg = _watchersService.DeleteWatcher(watcherId, userInfo.Id);
      return Ok(deleteMsg);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}