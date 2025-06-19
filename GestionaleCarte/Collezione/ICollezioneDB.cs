public interface ICollezioneDb
{
    Collezione? TrovaPerUtenteId(int UtenteId);
    bool CreaCollezione(int utenteId);
}