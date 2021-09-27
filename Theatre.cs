using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreatreProject
{
    public delegate void priceCutEvent(Double temVar);

    // The Theatre class uses pricing model to calculate ticket price
    class Theatre
    {
        // Number of Tickets
        private int ticketNumber;
        // The final purchased Amount
        private int ticketsBought;
        // Price of tickets
        private double ticketPrice;
        //price cute event, link event to delagate
        public static event priceCutEvent priceCut;
        // Initiating the counter that keeps track of number of price cuts 
        public int discount = 0;

        //Theatre theatre1 = new Theatre();

        static Random rando = new Random();

        public Theatre()
        {
            //Price of tickets
            ticketPrice = 150;
            //Number of tickets avaliable 
            ticketNumber = 60;
            //Initate discount count
            discount = 0;
            //Initating final purchase amount
            ticketsBought = 0; 
        }
        public void TheatreFunction()
        {
            // After discount count is equal to 10 or greater than 10 than terminate the airLine thread
            while (discount <= 3)
            {
                Thread.Sleep(100);
                // To call the pricing model
                PricingModel();

                OrderClass ticketOrder = Program.cellBuffer.GetOneCell(Thread.CurrentThread.Name);

                Console.WriteLine("Name my order gonna track {0}", ticketOrder.GetSenderId());

                ticketPrice = ticketOrder.GetPrice();
                // to process the request that was received by the threatres
                if (ticketOrder != null)
                {
                    ticketsBought++;

                    ticketNumber -= ticketOrder.GetTotal();

                    OrderProcess processHandler = new OrderProcess(ticketOrder);

                    Thread processThread = new Thread(new ThreadStart(processHandler.Process));

                    processThread.Start();
                }
            }
            Program.theatreThreadCounter--;
        }

        public void ChangePrice(double newPrice)
        {

            if (newPrice < ticketPrice && priceCut != null)
            {
                Console.WriteLine("The value of TicketPrice is {0}", ticketPrice);
                priceCut(newPrice);
                //Will increment the number of price cuts 
                discount++;
                
                //Will et the new ticket price 
                ticketPrice = newPrice;
            }
            //In case mo price cut event occurs 
            else
            {
                
                //Will sets the new ticket price 
                ticketPrice = newPrice;
            }
        }


        //To decide the price of tickets (between 40 and 200) 
        public double PricingModel()
        {
            double changedPrice = 0;
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(500);
                //pricing model that decides randomly 
                changedPrice = rando.Next(40, 200);
                ChangePrice(changedPrice);
            }
            return changedPrice;
        }
    }
}
