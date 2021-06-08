using NRulesWithAspNetCore.Models;
using NRulesWithAspNetCore.Services;
using NRules.Fluent.Dsl;
using System;
using System.Diagnostics;

namespace NRulesWithAspNetCore.Rules
{
    public class MalePersonRule : Rule
    {
        public MalePersonRule()
        {
            //Debugger.Break();

            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{nameof(MalePersonRule)} object is created.");
            Console.ForegroundColor = defaultColor;
        }
        //private readonly IPersonService _personService;

        //public MalePersonRule(IPrintService printService
        //    , IPersonService personService
        //    )
        //{
        //    Debugger.Break();
        //    _personService = personService;
        //}

        public override void Define()
        {
            IPersonService personService = null;

            IServiceProvider serviceProvider = null;


            Dependency()
                .Resolve(() => personService);

            Dependency()
                .Resolve(() => serviceProvider);

            Person person = null;

            When()
                .Match<Person>(() => person, c => c.IsMale);

            Then()
                // In both of the following cases, I get the same exception.
                // Cannot resolve scoped service 'IPersonService' from root provider.
                // What should I do?
                //.Do(ctx => Console.WriteLine(personService.Id))
                // Or the following, same exception.
                .Do(ctx => Console.WriteLine(((IPersonService)serviceProvider.GetService(typeof(IPersonService))).Id))
                ;
        }
    }
}
