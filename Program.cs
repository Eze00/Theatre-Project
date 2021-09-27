using System;
using System.Threading;

namespace ThreatreProject
{
    class Program
    {
        // MultiCellBuffer object
        public static MultiCellBuffer cellBuffer = new MultiCellBuffer();
        // counter
        public static int theatreThreadCounter = 0;

        public static void Main()
        {

            // Initialize the variable for theatre objects
            Theatre cinema = new Theatre();
            // Initialize the variable for  ticketBrokers objects
            TicketBroker buyer = new TicketBroker();

            // start Threads
            Thread[] theatre = new Thread[1];

            for (int i = 0; i < 1; i++)
            {
                theatre[i] = new Thread(new ThreadStart(cinema.TheatreFunction));

                theatre[i].Name = "Theatre " + i;

                theatre[i].Start();

                theatreThreadCounter++;
            }

            //create threads N = 5 threads
            Thread[] TicketBrokers = new Thread[5];

            for (int j = 0; j < 5; j++)
            {
                TicketBrokers[j] = new Thread(new ThreadStart(buyer.DoTicketBroker));

                TicketBrokers[j].Name = "Ticket Broker" + j;

                //where thread will start
                TicketBrokers[j].Start();
            }

            // Connects theatre event to the ticket brokers
            Theatre.priceCut += new priceCutEvent(buyer.priceDropHandler);

            // Wait for threads to start. block on the semaphore
            Thread.Sleep(550);

            Console.ReadLine();
        }
    }
}
