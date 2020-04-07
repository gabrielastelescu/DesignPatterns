using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    //    Based on this idea, Robert C. Martin’s definition of the Dependency Inversion Principle consists of two parts:

    //    High-level modules should not depend on low-level modules. Both should depend on abstractions.
    //    Abstractions should not depend on details. Details should depend on abstractions.

    public class DependencyInversionPrinciple
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name;
        }

        // low-level module
        public class Relationships: IRelationShipBrowser
        {
            private List<(Person, Relationship, Person)> relations 
                = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            IEnumerable<Person> IRelationShipBrowser.FindAllChildrenOf(string name)
            {
                foreach (var r in relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent))
                {
                    yield return r.Item3;
                }
            }

            // !!! WRONG, high level will access low level, solve it through an interface!!!
            //public List<(Person, Relationship, Person)> Relations => relations;

        }

        public interface IRelationShipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        public class Research
        {
            // WRONG!!
            //public Research(Relationships relationships)
            //{
            //var relations = relationships.Relations;
            //foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
            //{
            //   Console.WriteLine($"John has a child called {r.Item3.Name}");
            //}
            // }

            // CORRECTION
            public Research(IRelationShipBrowser browser)
            {
                foreach (var p in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}");
                }
            }

            static void Mainn(string[] args)
            {
                var parent = new Person { Name = "John"};
                var child1 = new Person { Name = "Chris" };
                var child2 = new Person { Name = "Mary" };

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                new Research(relationships);
            }
        }


    }
}
