




namespace post_it_sharp.Repositories;

public class WatchersRepository
{
  private readonly IDbConnection _db;

  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO watchers
    (account_id, album_id)
    VALUES
    (@AccountId, @AlbumId);

    SELECT
    *
    FROM watchers
    WHERE id = LAST_INSERT_ID()
    ;";
    Watcher watcher = _db.Query<Watcher>(sql, watcherData).SingleOrDefault();
    return watcher;
  }

  internal void DeleteWatcher(int watcherId)
  {
    string sql = @"
    DELETE FROM watchers
    WHERE id = @watcherId
    LIMIT 1
    ;";
    _db.Execute(sql, new { watcherId });
  }

  internal List<WatcherAlbum> GetAlbumsImWatching(string userId)
  {
    string sql = @"
    SELECT
      watchers.id AS watcherId,
      watchers.account_id,
      albums.*,
      accounts.*
    FROM watchers
    JOIN albums ON watchers.album_id = albums.id
    JOIN accounts ON albums.creator_id = accounts.id
    WHERE account_id = @userId
    ;";
    List<WatcherAlbum> watcherAlbums = _db.Query(sql,
    (WatcherAlbum watcherAlbum, Profile profile) =>
    {
      watcherAlbum.Creator = profile;
      return watcherAlbum;
    },
     new { userId }).ToList();
    return watcherAlbums;
  }

  internal List<WatcherProfile> GetAlbumWatchers(int albumId)
  {
    // NOTE we renamed watchers.id to watcherId, so the watcher and account table didn't have overlap between member names. WatcherId is also a member we needed on our WatcherProfile
    string sql = @"
    SELECT
      watchers.id AS watcherId,
      watchers.album_id,
      accounts.*
    FROM watchers
    JOIN accounts ON watchers.account_id = accounts.id
    WHERE album_id = @albumId
    ;";
    List<WatcherProfile> watchers = _db.Query<WatcherProfile>(sql, new { albumId }).ToList();
    return watchers;
  }

  internal Watcher GetWatcher(int watcherId)
  {
    string sql = @"
    SELECT
    *
    FROM watchers
    WHERE id = @watcherId
    ";
    Watcher watcher = _db.Query<Watcher>(sql, new { watcherId }).SingleOrDefault();
    return watcher;
  }
}