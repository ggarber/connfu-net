using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnFu;

namespace ConnFuConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connFu = new ConnFuClient("TOKEN");
            connFu.Twitter += (sender, ev) =>
                {
                    Console.WriteLine("Look! My dude {0} posted {1} to Twitter", ev.From, ev.Content);
                };

            Console.ReadLine();
        }
    }
}
