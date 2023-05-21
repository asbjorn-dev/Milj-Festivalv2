using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IPersonRepository
    {
        Task<Person> GetPerson(int brugerId);
        Task UpdatePerson(Person person);
    }
}
