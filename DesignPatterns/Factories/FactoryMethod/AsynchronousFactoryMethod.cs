using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factories.FactoryMethod
{
    public class Foo
    {
        // 1st Step: make the constructor private!!!
        private Foo()
        {

        }

        // 2nd Step
        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        // 3rd Step: add a factory method
        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    public class Demo
    {
        public static async void Mainn(string[] args)
        {
            Foo foo = await Foo.CreateAsync();
        }
    }
}
