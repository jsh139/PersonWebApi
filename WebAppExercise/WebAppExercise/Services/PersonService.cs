using Newtonsoft.Json;
using WebAppExercise.Models;

namespace WebAppExercise.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetPeople(bool includePII);
        Task<Person> GetPerson(long id, bool includePII);
    }

    public class PersonService : IPersonService
    {
        public async Task<List<Person>> GetPeople(bool includePII)
        {
            var text = await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}people.json");

            var people = JsonConvert.DeserializeObject<List<Person>>(text);

            if (!includePII)
            {
                people?.ForEach(p =>
                {
                    p.Age = null;
                    p.SSN = "XXXXXX";
                });
            }

            return people ?? new List<Person>();
        }

        public async Task<Person> GetPerson(long id, bool includePII)
        {
            var people = await GetPeople(includePII);

            return people.FirstOrDefault(p => p.Id == id);
        }
    }
}
