/// <summary>
/// Rappresenta un'espansione del gioco di carte Pokémon
/// </summary>
public class Espansione
{
    /// <summary>
    /// Identificatore univoco dell'espansione nel database
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Nome dell'espansione (es. "Base Set", "Jungle", etc.)
    /// </summary>
    public string Nome { get; set; }
    
    /// <summary>
    /// Anno di rilascio dell'espansione
    /// </summary>
    public DateTime Anno { get; set; }
}