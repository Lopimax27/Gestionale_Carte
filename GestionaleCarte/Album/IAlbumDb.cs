public interface IAlbumDb
{
    bool AggiungiCarta(int albumId, int cartaId);
    bool RimuoviCarta(int albumId, int cartaId);
    bool CartaGiaPresente(int albumId, int cartaId);
    int? TrovaIdCarta(string nome, int espansioneId);
    
}