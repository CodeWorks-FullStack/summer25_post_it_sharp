

namespace post_it_sharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
      INSERT INTO pictures
      (img_url, creator_id, album_id)
      VALUES
      (@ImgUrl, @CreatorId, @AlbumId);

      SELECT
        pictures.*,
        accounts.*
      FROM pictures
      JOIN accounts ON pictures.creator_id = accounts.id
      WHERE pictures.id = LAST_INSERT_ID()
    ;";

    Picture picture = _db.Query(sql,
    (Picture picture, Profile profile) =>
    {
      picture.Creator = profile;
      return picture;
    },
     pictureData).SingleOrDefault();
    return picture;
  }

  internal List<Picture> GetPicturesInAlbum(int albumId)
  {
    string sql = @"
    SELECT
      pictures.*,
      accounts.*
    FROM pictures
    JOIN accounts ON pictures.creator_id = accounts.id
    WHERE pictures.album_id = @albumId
    ;";

    List<Picture> pictures = _db.Query(sql,
    (Picture picture, Profile profile) =>
    {
      picture.Creator = profile;
      return picture;
    },
    new { albumId }).ToList();
    return pictures;
  }
}