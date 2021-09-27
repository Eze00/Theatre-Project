using System;
using System.Collections.Generic;
using System.Text;

namespace ThreatreProject
{
    // orderclass
    class OrderClass
    {
        /*
        * variables I'm going to work with
        */
        private string senderId;
        private int cardNumber;
        private int total;
        private double price;

        /*
        * the gettter methods
        */
        public void SetSenderId(string senderId)
        {
            this.senderId = senderId;
        }
        public void SetCardNumber(int cardNumber)
        {
            this.cardNumber = cardNumber;
        }
        public void SetTotal(int total)
        {
            this.total = total;
        }
        public void SetPrice(double price)
        {
            this.price = price;
        }

        /*
         * the gettter methods
         */
        public string GetSenderId()
        {
            return senderId;
        }
        public int GetCardNumber()
        {
            return cardNumber;
        }
        public int GetTotal()
        {
            return total;
        }
        public double GetPrice()
        {
            return price;
        }

    }
}
