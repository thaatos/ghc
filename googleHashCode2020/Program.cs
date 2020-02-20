using System;

namespace googleHashCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var parser = new DataParser();
            var data = parser.ReadData("googleHashCode2020.DataInput.c_incunabula.txt");
            Console.ReadLine();
        }
    }
}
