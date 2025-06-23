using static Carta;

/// <summary>
/// Interfaccia per la gestione delle operazioni di database relative alle carte Pokémon
/// </summary>
public interface ICartaDB
{
    /// <summary>
    /// Trova una carta specifica tramite nome Pokémon e ID espansione
    /// </summary>
    /// <param name="nomePokemon">Nome del Pokémon da cercare</param>
    /// <param name="idEspansione">ID dell'espansione di appartenenza</param>
    /// <returns>Oggetto Carta se trovato, null altrimenti</returns>
    Carta? TrovaCarta(string nomePokemon, int idEspansione);

    /// <summary>
    /// Recupera tutte le carte appartenenti a una specifica espansione
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <returns>Lista delle carte dell'espansione</returns>
    List<Carta> TrovaPerEspansione(string nomeEspansione);

    /// <summary>
    /// Inserisce una nuova carta nel database
    /// </summary>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="tipoPokemon">Tipo elementale della carta</param>
    /// <param name="raritaCarta">Rarità della carta</param>
    /// <param name="prezzoCarta">Prezzo della carta in euro</param>
    /// <param name="IsReverse">Flag per carta Reverse</param>
    /// <param name="IdEspansione">ID dell'espansione di appartenenza</param>
    /// <param name="url">URL dell'immagine della carta</param>
    /// <returns>True se l'inserimento ha successo, False altrimenti</returns>
    bool InserisciCarta(string nomePokemon, Tipo tipoPokemon, Rarita raritaCarta, decimal prezzoCarta, bool IsReverse, int IdEspansione, string url);

    /// <summary>
    /// Rimuove una carta dal database
    /// </summary>
    /// <param name="Id">ID della carta da rimuovere</param>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="espansioneCarta">Nome dell'espansione</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
    bool RimuoviCarta(int Id, string nomePokemon, string espansioneCarta);
}