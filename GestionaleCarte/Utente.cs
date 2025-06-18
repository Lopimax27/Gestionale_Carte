using MySql.Data.MySqlClient;
public class Utente
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Collezione CollezioneUtente { get; set; }

    public Utente(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
        CollezioneUtente = new Collezione();
    }

    public void VisualizzaCollezione()
    {
        CollezioneUtente.Visualizza();
    }

    public void CreaAlbum(string nomeAlbum)
    {
        CollezioneUtente.AggiungiAlbum(new Album(nomeAlbum));
    }

    public void EliminaAlbum(string nomeAlbum)
    {
        CollezioneUtente.RimuoviAlbum(nomeAlbum);
    }

    public void MostraAlbum(string nomeAlbum)
    {
        CollezioneUtente.MostraAlbum(nomeAlbum);
    }
}

