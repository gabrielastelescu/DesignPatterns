using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    public class OpenClosedPrinciple
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }

            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Color = color;
                Size = size;
            }

            public override string ToString()
            {
                return $" - {this.Name} is {this.Color}";
            }
        }

        /* !!! THE WRONG WAY, KEEP MODIFYING THE SAME CLASS TO ADD NEW FUNCTIÓNALITY !!!!*/
        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                return products.Where(p => p.Size == size);
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                return products.Where(p => p.Color == color);
            }

            // WRONG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
            {
                return products.Where(p => p.Color == color && p.Size == size);
            }
        }

        // START MAKING THE FUNCTIONALITY EXTENDABLE
        public interface ISpecification<T> 
        {
            bool isSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> itmes, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool isSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool isSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first, second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(nameof(first));
                this.second = second ?? throw new ArgumentNullException(nameof(second));
            }

            public bool isSatisfied(T t)
            {
                return first.isSatisfied(t) && second.isSatisfied(t);
            }
        }

        public class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                return items.Where(i => spec.isSatisfied(i));
            }
        }

        static void Mainn(string[] args)
        {
            Console.WriteLine("\n\n**********  Open Closed Principle  **********");
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");
            foreach (var prod in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine(prod);
            }

            var bf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (var prod in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine(prod);
            }


            Console.WriteLine("Large blue items:");
            foreach (var prod in bf.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine(prod);
            }

        }
    }
}
