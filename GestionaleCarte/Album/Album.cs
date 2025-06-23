public class Album
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public Album(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public override string ToString()
    {
        return $"ID Album: {Id} | Nome Album {Nome}";
    }
}
