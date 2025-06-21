using System;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Parameters;

public class Carta
{
    public int Id { get; set; }

    public string NomePokemon { get; set; }

    public decimal Prezzo { get; set; }

    public Tipo TipoCarta { get; set; }

    public Rarita RaritaCarta { get; set; }

    public bool IsReverse { get; set; }

    public bool IsWanted{ get; set; }

    public bool IsObtained{ get; set; }

    public int IdEspansione { get; set; }

    public enum Tipo
    {
        Acqua,
        Fuoco,
        Erba,
        Elettro,
        Psico,
        Lotta,
        Buio,
        Metallo,
        Normale,
        Folletto,
        Drago
    }

    public enum Rarita
    {
        Comune,
        NonComune,
        Rara,
        RaraHolo,
        UltraRara,
        RaraSegreta
    }

    public override string ToString()
    {
        return $"Nome: {NomePokemon} | Tipo: {TipoCarta} | Rarità: {RaritaCarta} | Prezzo: €{Prezzo} | Reverse: {(IsReverse ? "✔" : "✘")} | Posseduta: {(IsObtained ? "✔" : "✘")} | Desiderata: {(IsWanted ? "✔" : "✘")}";

    }
}