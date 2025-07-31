




namespace post_it_sharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repo;

  public AlbumsService(AlbumsRepository repo)
  {
    _repo = repo;
  }



  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repo.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    List<Album> albums = _repo.GetAllAlbums();
    return albums;
  }

  internal Album GetOneAlbumById(int albumId)
  {
    Album album = _repo.GetOneAlbumById(albumId);
    if (album == null) throw new Exception($"No Album with id {albumId}");
    return album;
  }

  internal Album ArchiveAlbumById(int albumId, string userId)
  {
    Album albumToArchive = GetOneAlbumById(albumId);
    if (albumToArchive.CreatorId != userId) throw new Exception($"That's my purse I don't know you!");

    albumToArchive.Archived = !albumToArchive.Archived;
    _repo.UpdateAlbum(albumToArchive);
    albumToArchive.UpdatedAt = new DateTime(); // change updated at so you don't have to pull it from the database again, BUT you could just also pull it from the database again anyways, not necessary at all

    return albumToArchive;
  }
}