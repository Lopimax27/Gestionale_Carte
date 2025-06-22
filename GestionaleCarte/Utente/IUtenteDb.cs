public interface IUtenteDb
{
    bool Esiste(string username, string email);
    bool Inserisci(string username, string email, string passwordHash);
    Utente? TrovaPerUsername(string username);
    string? TrovaPasswordHash(int utenteId);
}