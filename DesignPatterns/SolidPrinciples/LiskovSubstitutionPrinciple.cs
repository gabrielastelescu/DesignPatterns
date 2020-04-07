using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    public class LiskovSubstitutionPrinciple
    {
        public class Rectangle
        {
            //public int Width { get; set; }
            //public int Height { get; set; }

            // !!!Correction for Liskov principle (properties->virtual):
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }
            public Rectangle()
            {
                    
            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        public class Square : Rectangle
        {
            //public new int Width { set { base.Width = base.Height = value; } }
            //public new int Height { set { base.Width = base.Height = value; } }
            // !!!Correction for Liskov principle (new -> override):
            public override int Width { set { base.Width = base.Height = value; } }
            public override int Height { set { base.Width = base.Height = value; } }
        }

        static void Mainn(string[] args)
        {
            Console.WriteLine("\n\n**********  Liskov Substitution Principle  **********");
            static int Area(Rectangle r) => r.Width * r.Height;

            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");

            // now, let's chnage something
            Rectangle sq2 = new Square();
            sq2.Width = 4;
            Console.WriteLine($"{sq2} has area {Area(sq2)}");
            Console.ReadKey();
        }
    }
}
