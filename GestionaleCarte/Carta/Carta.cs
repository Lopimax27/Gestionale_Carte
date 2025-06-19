using System;
using MySql.Data.MySqlClient;

public class Carta
{
    public int Id { get; set; }

    public string NomePokemon { get; set; }

    public decimal Prezzo { get; set; }

    public Tipo TipoCarta { get; set; }

    public Rarita RaritaCarta { get; set; }

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
        Non_Comune,
        Rara,
        Rara_Holo,
        Ultra_Rara,
        Rara_Segreta
    }


}