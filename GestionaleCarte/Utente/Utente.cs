public class Utente
{
    public int UtenteId { get; }
    public bool IsAdmin { get; }

    public Utente(int utenteId, bool isAdmin)
    {
        UtenteId = utenteId;
        IsAdmin = isAdmin;
    }
}
