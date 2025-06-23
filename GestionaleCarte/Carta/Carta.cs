using System;

/// <summary>
/// Rappresenta una carta del gioco Pokémon con tutte le sue proprietà e caratteristiche
/// </summary>
public class Carta
{
    /// <summary>
    /// Identificatore univoco della carta nel database
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome del Pokémon presente sulla carta
    /// </summary>
    public string NomePokemon { get; set; }

    /// <summary>
    /// Prezzo della carta in euro
    /// </summary>
    public decimal Prezzo { get; set; }

    /// <summary>
    /// Tipo elementale della carta (Acqua, Fuoco, Erba, etc.)
    /// </summary>
    public Tipo TipoCarta { get; set; }

    /// <summary>
    /// Rarità della carta (Comune, Rara, UltraRara, etc.)
    /// </summary>
    public Rarita RaritaCarta { get; set; }

    /// <summary>
    /// Indica se la carta è di tipo Reverse (ha un effetto speciale)
    /// </summary>
    public bool IsReverse { get; set; }

    /// <summary>
    /// Indica se l'utente desidera ottenere questa carta
    /// </summary>
    public bool IsWanted { get; set; }

    /// <summary>
    /// Indica se l'utente possiede questa carta
    /// </summary>
    public bool IsObtained { get; set; }

    /// <summary>
    /// Identificatore dell'espansione a cui appartiene la carta
    /// </summary>
    public int IdEspansione { get; set; }

    /// <summary>
    /// Enumerazione che definisce i tipi elementali delle carte Pokémon
    /// </summary>
    public enum Tipo
    {
        Acqua,      // Tipo Acqua
        Fuoco,      // Tipo Fuoco
        Erba,       // Tipo Erba
        Elettro,    // Tipo Elettrico
        Psico,      // Tipo Psichico
        Lotta,      // Tipo Lotta
        Buio,       // Tipo Buio
        Metallo,    // Tipo Metallo
        Normale,    // Tipo Normale
        Folletto,   // Tipo Folletto
        Drago       // Tipo Drago
    }

    /// <summary>
    /// Enumerazione che definisce i livelli di rarità delle carte
    /// </summary>
    public enum Rarita
    {
        Comune,         // Carta comune
        NonComune,      // Carta non comune
        Rara,           // Carta rara
        RaraHolo,       // Carta rara olografica
        UltraRara,      // Carta ultra rara
        RaraSegreta     // Carta rara segreta
    }

    /// <summary>
    /// Restituisce una rappresentazione testuale formattata della carta
    /// </summary>
    /// <returns>Stringa che contiene tutte le informazioni della carta</returns>
    public override string ToString()
    {
        // Formatta le informazioni della carta con simboli per i booleani
        return $"Nome: {NomePokemon} | Tipo: {TipoCarta} | Rarità: {RaritaCarta} | Prezzo: {Prezzo}€ | Reverse: {(IsReverse ? "✔" : "✘")} | Posseduta: {(IsObtained ? "✔" : "✘")} | Desiderata: {(IsWanted ? "✔" : "✘")}";
    }
}