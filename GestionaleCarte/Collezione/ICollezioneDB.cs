public interface ICollezioneDb
{
    Collezione? TrovaPerUtenteId(int utenteId);
    Album? TrovaPerNomeCollezioneId(int collezioneId,string nomeAlbum);
    bool CreaCollezione(int utenteId, string nomeCollezione);
    bool CreaAlbum(int utenteId,int collezioneId, string nomeAlbum);
}