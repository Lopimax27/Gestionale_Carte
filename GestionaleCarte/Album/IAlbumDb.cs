public interface IAlbumDb
{
    bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione,bool isObtained,bool isWanted);
    bool RimuoviCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione);
    bool CartaGiaPresente(int idAlbum, int idCarta);
    int? TrovaIdCarta(string nomePokemon, int idEspansione);
    decimal ValoreAlbum(int idAlbum);
    List<Carta> ListaCarte(int idAlbum);
}