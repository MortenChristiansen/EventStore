using Demo.Commands;
using Demo.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var exchange = "New York Stock Exchange";

            var registerBrokerService = new RegisterBrokerService();
            registerBrokerService.Handle(new RegisterBrokerCommand("John Parker", exchange));
            registerBrokerService.Handle(new RegisterBrokerCommand("Tina Reeds", exchange));

            var rand = new Random();
            var price = 150d;
            var updateStockPriceService = new UpdateStockPriceService();

            while(true)
            {
                Thread.Sleep(1000);
                var n = (rand.NextDouble() / 10) - 0.05;
                price += n * price;

                updateStockPriceService.Handle(new UpdateStockPriceCommand("MSN", exchange, (decimal)price, "USD"));
            }
        }
    }
}
