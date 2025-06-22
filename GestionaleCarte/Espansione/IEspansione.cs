public interface IEspansioneDb
{
    Espansione? TrovaPerNome(string nomeEspansione);
    bool InserisciEspansione(string nomeEspansione, DateTime anno);
    bool RimuoviEspansione(string nomeEspansione);
}