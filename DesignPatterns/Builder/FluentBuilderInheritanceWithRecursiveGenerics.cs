using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    public class FluentBuilderInheritanceWithRecursiveGenerics
    {
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public class Builder : PersonJobBuilder<Builder>
            { 
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person();
            public Person Build()
            {
                return person;
            }

        }
        public class PersonInfoBuilder<T> : PersonBuilder where T: PersonInfoBuilder<T>
        {
           

            public T Called(string name)
            {
                person.Name = name;

                return (T) this;

            }
        }

        public class PersonJobBuilder<T>: PersonInfoBuilder<PersonJobBuilder<T>> where T: PersonJobBuilder<T>
        {
            public T WorksAt(string position)
            {
                person.Position = position;
                return (T) this;
            }
        }

        internal class Program
        {
            public static void Mainn(string[] args)
            {
                var me = Person.New
                     .Called("dmitri")
                     .WorksAt("quant")
                     .Build();
                Console.WriteLine(me);
                    
            }
        }
    }
}
