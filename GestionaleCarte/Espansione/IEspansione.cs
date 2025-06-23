/// <summary>
/// Interfaccia per la gestione delle operazioni di database relative alle espansioni
/// </summary>
public interface IEspansioneDb
{
    /// <summary>
    /// Trova un'espansione tramite il suo nome
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione da cercare</param>
    /// <returns>Oggetto Espansione se trovato, null altrimenti</returns>
    Espansione? TrovaPerNome(string nomeEspansione);
    
    /// <summary>
    /// Inserisce una nuova espansione nel database
    /// </summary>
    /// <param name="nomeEspansione">Nome dell'espansione</param>
    /// <param name="anno">Anno di rilascio dell'espansione</param>
    /// <returns>True se l'inserimento ha successo, False altrimenti</returns>
    bool InserisciEspansione(string nomeEspansione, DateTime anno);
    
    /// <summary>
    /// Rimuove un'espansione dal database
    /// </summary>
    /// <param name="idEspansione">ID dell'espansione da rimuovere</param>
    /// <returns>True se la rimozione ha successo, False altrimenti</returns>
    bool RimuoviEspansione(int idEspansione);
}