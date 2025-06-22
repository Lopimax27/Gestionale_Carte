using System.Security.Policy;
using static Carta;

public interface ICartaDB
{
    Carta? TrovaCarta(string nomePokemon, int idEspansione);
    List<Carta> TrovaPerEspansione(string nomeEspansione);
    bool InserisciCarta(string nomePokemon, Tipo tipoPokemon, Rarita raritaCarta, decimal prezzoCarta, bool IsReverse, int IdEspansione, string url);
    bool RimuoviCarta(int Id, string nomePokemon, string espansioneCarta);
    

}