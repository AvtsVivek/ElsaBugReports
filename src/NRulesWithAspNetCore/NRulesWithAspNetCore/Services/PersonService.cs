using NRulesWithAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NRulesWithAspNetCore.Services
{
    public interface IPersonService
    {
        public Guid Id { get; }
        public void WritePersonInfo(Person person);
        public List<Person> MalePersonList { get; set; }
    }
    public class PersonService : IPersonService
    {
        public Guid Id { get; }
        public List<Person> MalePersonList { get ; set ; }

        public PersonService()
        {
            Debugger.Break();
            Id = Guid.NewGuid();
            Console.WriteLine($"Person Service Created with Id {Id}");
            MalePersonList = new List<Person>();
        }

        public void WritePersonInfo(Person person)
        {
            MalePersonList.Add(person);

            Console.WriteLine($"{person.Name} is male.");
        }
    }
}
