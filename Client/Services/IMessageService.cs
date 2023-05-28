using MiljøFestivalv2.Shared;

// Definition af interface IMessageService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IMessageService
{
    Task<Msg_board[]> HentAlleBeskeder();

    Task TilføjBesked(Msg_board msg);
}
