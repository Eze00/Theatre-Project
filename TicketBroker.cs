using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreatreProject
{
    class TicketBroker
    {
        //Keeps track of my current prices 
        private double currentPrice;
        //indicates if a request is made
        private bool placedOrder;
        //keeps track of how many tickets to buy 
        private OrderClass orderClass;
        //create random number for credit card number 
        static Random rando = new Random();


        public TicketBroker()
        {
            currentPrice = 350;
            placedOrder = false;
            orderClass = null;
        }

        public void DoTicketBroker()
        {
  
            while (Program.theatreThreadCounter > 0)
            {
                // Check if an request should be placed or not
                if (placedOrder == true)
                {
                    orderClass.SetSenderId(Thread.CurrentThread.Name);

                    // print request 
                    Console.WriteLine(orderClass.GetSenderId() + " just requested: " + orderClass.GetTotal()
                    + " tickets for: $" + orderClass.GetPrice() + " dollars each");

                  
                    Program.cellBuffer.SetOneCell(orderClass);

                    // Update price by calling the access method 
                    currentPrice = orderClass.GetPrice();
                    placedOrder = false;
                }
            }
        }

        public void priceDropHandler(Double newSetPrice)
        {
            //didn't place an order
            if (!placedOrder)
            {
                // initializing the amount of order
                int ticketsBought = 0; 
                //The change of price
                double priceDifference = currentPrice - newSetPrice; 
                Console.WriteLine("The current price is: {0} ", priceDifference);
                Console.WriteLine(priceDifference);

                Console.WriteLine("The price has dropped {0} ", priceDifference + " dollars");

                // Only proceed if new price is less than current price
                if (priceDifference > 0)
                {
                    if (priceDifference < 50)
                    {
                        ticketsBought = 5;
                    }
                    else if (priceDifference < 200)
                    {
                        ticketsBought = 10;
                    }
                    else if (priceDifference < 300)
                    {
                        ticketsBought = 15;
                    }
                    else if (priceDifference <= 400)
                    {
                        ticketsBought = 20;
                    }

                    // Creates a new request
                    orderClass = new OrderClass();
                    // ramdomly chooses
                    int cardNo = rando.Next(5000, 7000);
                    //the list of the credit card number 
                    orderClass.SetCardNumber(cardNo);

                    //List of the theatre id
                    orderClass.SetSenderId(Thread.CurrentThread.Name);

                    //List of the amount ordered
                    orderClass.SetTotal(ticketsBought);

                    //List of the price
                    orderClass.SetPrice(newSetPrice);

                    currentPrice = orderClass.GetPrice();

                    //Sets the boolean to true 
                    placedOrder = true;
                }
            }

        }
    }
}
