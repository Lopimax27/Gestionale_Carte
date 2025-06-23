/// <summary>
/// Interfaccia per la gestione delle operazioni di database relative agli album
/// </summary>
public interface IAlbumDb
{
    /// <summary>
    /// Aggiunge una carta a un album specifico
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <param name="idCarta">ID della carta</param>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <param name="isObtained">Flag che indica se la carta è posseduta</param>
    /// <param name="isWanted">Flag che indica se la carta è desiderata</param>
    /// <returns>True se l'aggiunta ha successo, False altrimenti</returns>
    bool AggiungiCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione, bool isObtained, bool isWanted);

    /// <summary>
    /// Rimuove una carta da un album specifico
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <param name="idCarta">ID della carta</param>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
    bool RimuoviCarta(int idAlbum, int idCarta, string nomePokemon, string nomeEspansione);

    /// <summary>
    /// Verifica se una carta è già presente in un album
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <param name="idCarta">ID della carta</param>
    /// <returns>True se la carta è già presente, False altrimenti</returns>
    bool CartaGiaPresente(int idAlbum, int idCarta);

    /// <summary>
    /// Trova l'ID di una carta tramite nome Pokémon e ID espansione
    /// </summary>
    /// <param name="nomePokemon">Nome del Pokémon</param>
    /// <param name="idEspansione">ID dell'espansione</param>
    /// <returns>ID della carta se trovata, null altrimenti</returns>
    int? TrovaIdCarta(string nomePokemon, int idEspansione);

    /// <summary>
    /// Calcola il valore totale di tutte le carte in un album
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <returns>Valore totale dell'album in euro</returns>
    decimal ValoreAlbum(int idAlbum);

    /// <summary>
    /// Recupera tutte le carte presenti in un album
    /// </summary>
    /// <param name="idAlbum">ID dell'album</param>
    /// <returns>Lista delle carte nell'album</returns>
    List<Carta> ListaCarte(int idAlbum);
}