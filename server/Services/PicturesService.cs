

namespace post_it_sharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repo;

  public PicturesService(PicturesRepository repo)
  {
    _repo = repo;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Picture picture = _repo.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> GetPicturesInAlbum(int albumId)
  {
    List<Picture> pictures = _repo.GetPicturesInAlbum(albumId);
    return pictures;
  }
}