public interface ICollezioneDb
{
    Collezione? TrovaPerUtenteId(int utenteId);
    Album? TrovaPerNomeCollezioneId(int collezioneId, string nomeAlbum);
    bool CreaCollezione(int utenteId, string nomeCollezione);
    bool CreaAlbum(int collezioneId, string nomeAlbum);
    List<Album>? VisualizzaCollezione(int idCollezione);
    bool EliminaALbum(int collezioneId, int albumId);
}