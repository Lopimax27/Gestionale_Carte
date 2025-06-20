public interface IAlbumDb
{
    bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, int idEspansione);
    bool RimuoviCarta(int idAlbum, int idCarta, string nomePokemon, int idEspansione);
    bool CartaGiaPresente(int idAlbum, int idCarta);
    int? TrovaIdCarta(string nomePokemon, int idEspansione);
    
}