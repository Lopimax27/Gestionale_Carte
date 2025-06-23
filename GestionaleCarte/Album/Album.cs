/// <summary>
/// Rappresenta un album contenente carte Pokémon all'interno di una collezione
/// </summary>
public class Album
{
    /// <summary>
    /// Identificatore univoco dell'album nel database
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome dell'album scelto dall'utente
    /// </summary>
    public string Nome { get; set; }

    /// <summary>
    /// Costruttore per creare un nuovo album
    /// </summary>
    /// <param name="id">ID univoco dell'album</param>
    /// <param name="nome">Nome dell'album</param>
    public Album(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    /// <summary>
    /// Restituisce una rappresentazione testuale dell'album
    /// </summary>
    /// <returns>Stringa con ID e nome dell'album</returns>
    public override string ToString()
    {
        return $"ID Album: {Id} | Nome Album: {Nome}";
    }
}
