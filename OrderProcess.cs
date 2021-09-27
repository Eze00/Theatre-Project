using System;
using System.Collections.Generic;
using System.Text;

namespace ThreatreProject
{
    class OrderProcess
    {
        private OrderClass order;

        private const double areaTax = 2.50;

        private const double areaPrice = 5.75;

        public OrderProcess(OrderClass order)
        {
            this.order = order;

        }

        public void Process()
        {
            //Checks validity of card. To be valid card has to be between 5000 and 7000. 
            if (Validation(order.GetCardNumber()) == true)
            {
                //card is valid so each order processing thread will calculate the final costs
                Double grandTotal = (order.GetTotal() * order.GetPrice() + areaPrice) * areaTax;

                Console.WriteLine("Total amount of request order to " + order.GetSenderId() + " is $" + String.Format("{0:0.00}", grandTotal) + " dollars, the unit price would of been " + order.GetPrice());

                Program.cellBuffer.SetOneCell(order);
            }
            else
            {
                Console.WriteLine("Cannot process order. Credit card number is invalid!");
            }
        }
        // Needs to be between 5000 and 7000 to be valid
        private bool Validation(int CardNumber)
        {
            
            return (CardNumber >= 5000 && CardNumber <= 7000);
        }
    }
}
