using BCrypt.Net;
using MySql.Data.MySqlClient;

public class ServiziUtente
{
    private readonly IUtenteDb _utenteDb;

    public ServiziUtente(IUtenteDb utenteDb)
    {
        _utenteDb = utenteDb;
    }

    public bool Registra(string username, string email, string password)
    {
        if (_utenteDb.Esiste(username, email))
            return false;

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return _utenteDb.Inserisci(username, email, passwordHash);
    }

    public Utente? Login(string username, string password)
    {
        var utente = _utenteDb.TrovaPerUsername(username);
        if (utente == null)
            return null;

        string sqlPassword = "SELECT password_hash FROM utente WHERE id_utente = @utenteId";
        using var cmd = new MySqlCommand(sqlPassword, utente.Connection);
        cmd.Parameters.AddWithValue("@utenteId", utente.UtenteId);

        using var rdr = cmd.ExecuteReader();
        if (!rdr.Read())
            return null;

        string passwordHash = rdr.GetString("password_hash");
        if (!BCrypt.Net.BCrypt.Verify(password, passwordHash))
            return null;

        return utente;
    }
}
