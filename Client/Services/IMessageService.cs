using MiljøFestivalv2.Shared;

// Definition af interface IMessageService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IMessageService
{
    // Metode til at hente alle beskeder. Den skal returnere et array af Msg_board objekter.
    Task<Msg_board[]> HentAlleBeskeder();

    // Metode til at tilføje en besked. Den tager et Msg_board objekt som argument.
    Task TilføjBesked(Msg_board msg);
}
