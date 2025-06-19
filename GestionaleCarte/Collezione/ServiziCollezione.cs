public class ServiziCollezione
{
    private readonly ICollezioneDb _collezioneDb;
    private readonly ServiziAlbum _serviziAlbum;

    public ServiziCollezione(ICollezioneDb collezioneDb, ServiziAlbum serviziAlbum)
    {
        _collezioneDb = collezioneDb;
        _serviziAlbum = serviziAlbum;
    }

    public Collezione OttieniCreaCollezione(int utenteId)
    {
        var collezione = _collezioneDb.TrovaPerUtenteId(utenteId);
        if (collezione == null)
        {
            _collezioneDb.CreaCollezione(utenteId);
            collezione = _collezioneDb.TrovaPerUtenteId(utenteId)!;//Punto esclamativo dice fidati capo non è nullo
        }
        return collezione;
    }

}