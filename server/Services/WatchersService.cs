

namespace post_it_sharp.Services;

public class WatchersService
{
  private readonly WatchersRepository _repo;
  private readonly AlbumsService _albumsService;

  public WatchersService(WatchersRepository repo, AlbumsService albumsService)
  {
    _repo = repo;
    _albumsService = albumsService;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    Album albumThatHopefullyExists = _albumsService.GetOneAlbumById(watcherData.AlbumId);
    // NOTE GeOneAlbumById already does a null check
    // if (albumThatHopefullyExists == null) throw new Exception($"Cannot create watcher, album {watcherData.AlbumId} does not exist");
    // NOTE check verify ownership of album object first (not a rule for this app but good to know)
    // if (albumThatHopefullyExists.CreatorId != watcherData.AccountId) throw new Exception("Cannot watch an album that doesn't belong to you");
    if (albumThatHopefullyExists.Archived == true) throw new Exception("Cannot watch an archived album");

    Watcher watcher = _repo.CreateWatcher(watcherData);
    return watcher;
  }

  internal string DeleteWatcher(int watcherId, string userId)
  {
    Watcher watcherToDelete = GetWatcher(watcherId);
    if (watcherToDelete.AccountId != userId) throw new Exception($"You don't own that, you can't delete it! 🤬");
    _repo.DeleteWatcher(watcherId);
    return $"watcher with id {watcherId} was deleted";
  }

  internal Watcher GetWatcher(int watcherId)
  {
    Watcher watcher = _repo.GetWatcher(watcherId);
    if (watcher == null) throw new Exception($"No watcher with id {watcherId}");
    return watcher;
  }

  internal List<WatcherAlbum> GetAlbumsImWatching(string userId)
  {
    List<WatcherAlbum> watcherAlbums = _repo.GetAlbumsImWatching(userId);
    return watcherAlbums;
  }

  internal List<WatcherProfile> GetAlbumWatchers(int albumId)
  {
    _albumsService.GetOneAlbumById(albumId); // really quick check to make sure the album does exist
    List<WatcherProfile> watchers = _repo.GetAlbumWatchers(albumId);
    return watchers;
  }
}