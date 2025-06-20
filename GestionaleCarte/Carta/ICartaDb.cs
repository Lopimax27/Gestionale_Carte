public interface ICartaDB
{
    Carta? TrovaCarta(string nomePokemon, int idEspansione);
    List<Carta> TrovaPerEspansione(string nomeEspansione);
}