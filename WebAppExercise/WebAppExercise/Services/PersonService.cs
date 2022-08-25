using Newtonsoft.Json;
using System.Reflection;
using WebAppExercise.Attributes;
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
                var type = typeof(Person);
                var sensitiveFields = type.GetProperties()
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(SensitiveDataAttribute)))
                    .ToList();

                people?.ForEach(p => RedactSensitiveData(type, p, sensitiveFields));
            }

            return people ?? new List<Person>();
        }

        private void RedactSensitiveData(Type type, Person person, List<PropertyInfo> sensitiveFields)
        {
            foreach (var field in sensitiveFields)
            {
                var prop = type.GetProperty(field.Name);

                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(person, null);
                }
            }
        }

        public async Task<Person> GetPerson(long id, bool includePII)
        {
            var people = await GetPeople(includePII);

            return people.FirstOrDefault(p => p.Id == id);
        }
    }
}
