public class ServiziAlbum
{
    public readonly IAlbumDb albumDb;

    public ServiziAlbum(IAlbumDb albumDb)
    {
        this.albumDb = albumDb;
    }
}
