



namespace post_it_sharp.Repositories;

public class AlbumsRepository
{

  private readonly IDbConnection _db;
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
      INSERT INTO albums
      (title, description, cover_img, category, creator_id)
      VALUES
      (@title, @description, @coverImg, @category, @creatorId);

     SELECT 
        albums.*,
        accounts.*
      FROM albums
      JOIN accounts ON albums.creator_id = accounts.id
      WHERE albums.id = LAST_INSERT_ID()
    ; ";
    Album album = _db.Query(sql,
    (Album album, Profile profile) =>
    {
      album.Description ??= $"{profile.Name} did not provide a description 🦄";
      album.Creator = profile;
      return album;
    },
    albumData).SingleOrDefault();
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT 
        albums.*,
        accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    ;";
    // NOTE if using a function for you map, you do have to explicitly define the types of the Query
    //---------------------<first select, second select, return type>
    List<Album> albums = _db.Query<Album, Profile, Album>(sql, MapCreator).ToList();
    return albums;
  }

  internal Album GetOneAlbumById(int albumId)
  {
    string sql = @"
    SELECT 
        albums.*,
        accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    WHERE albums.id = @albumId
    ;";
    Album album = _db.Query<Album, Profile, Album>(sql, MapCreator, new { albumId }).SingleOrDefault();
    return album;
  }

  // NOTE for just archiving data there is totally more stuff here than we need, but it might be handy to have the extra stuff in for future stuff
  internal void UpdateAlbum(Album albumUpdate)
  {
    string sql = @"
    UPDATE albums SET
      title = @title,
      description = @description,
      category = @category,
      archived = @archived
    WHERE id = @id
    ;";
    int rowsAffected = _db.Execute(sql, albumUpdate);
    if (rowsAffected > 1) throw new Exception($"Did you leave you database running? you might want to go catch it");
  }

  // NOTE instead of writing an anonymous function for you map, you can just give it a name and run it inside the dapper statement
  private Album MapCreator(Album album, Profile profile)
  {
    album.Description ??= $"{profile.Name} did not provide a description 🦄";
    album.Creator = profile;
    return album;
  }
}