using Demo.Commands;
using Demo.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchange = "New York Stock Exchange";

            var handler = new RegisterBrokerService();
            handler.Handle(new RegisterBrokerCommand("John Parker", exchange));
            handler.Handle(new RegisterBrokerCommand("Tina Reeds", exchange));

            Console.ReadLine();
        }
    }
}
