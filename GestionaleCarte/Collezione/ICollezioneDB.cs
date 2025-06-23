/// <summary>
/// Interfaccia per la gestione delle operazioni di database relative alle collezioni
/// </summary>
public interface ICollezioneDb
{
    /// <summary>
    /// Trova la collezione appartenente a un utente specifico
    /// </summary>
    /// <param name="utenteId">ID dell'utente</param>
    /// <returns>Oggetto Collezione se trovato, null altrimenti</returns>
    Collezione? TrovaPerUtenteId(int utenteId);
    
    /// <summary>
    /// Trova un album specifico tramite nome e ID collezione
    /// </summary>
    /// <param name="collezioneId">ID della collezione</param>
    /// <param name="nomeAlbum">Nome dell'album da cercare</param>
    /// <returns>Oggetto Album se trovato, null altrimenti</returns>
    Album? TrovaPerNomeCollezioneId(int collezioneId, string nomeAlbum);
    
    /// <summary>
    /// Crea una nuova collezione per un utente
    /// </summary>
    /// <param name="utenteId">ID dell'utente proprietario</param>
    /// <param name="nomeCollezione">Nome della collezione</param>
    /// <returns>True se la creazione ha successo, False altrimenti</returns>
    bool CreaCollezione(int utenteId, string nomeCollezione);
    
    /// <summary>
    /// Crea un nuovo album all'interno di una collezione
    /// </summary>
    /// <param name="collezioneId">ID della collezione</param>
    /// <param name="nomeAlbum">Nome dell'album</param>
    /// <returns>True se la creazione ha successo, False altrimenti</returns>
    bool CreaAlbum(int collezioneId, string nomeAlbum);
    
    /// <summary>
    /// Recupera tutti gli album di una collezione
    /// </summary>
    /// <param name="idCollezione">ID della collezione</param>
    /// <returns>Lista degli album o null se non presenti</returns>
    List<Album>? VisualizzaCollezione(int idCollezione);
    
    /// <summary>
    /// Elimina un album da una collezione
    /// </summary>
    /// <param name="collezioneId">ID della collezione</param>
    /// <param name="albumId">ID dell'album da eliminare</param>
    /// <returns>True se l'eliminazione ha successo, False altrimenti</returns>
    bool EliminaALbum(int collezioneId, int albumId);
}