/// <summary>
/// Rappresenta una collezione di carte Pokémon appartenente a un utente
/// </summary>
public class Collezione
{
    /// <summary>
    /// Identificatore univoco della collezione nel database
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificatore dell'utente proprietario della collezione
    /// </summary>
    public int UtenteId { get; set; }

    /// <summary>
    /// Nome della collezione scelto dall'utente
    /// </summary>
    public string Nome { get; set; }
}