using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreatreProject
{
    class MultiCellBuffer
    {
        
        public static Semaphore semaphore = new Semaphore(0, 2);
        private static OrderClass[] buffer = new OrderClass[2];
        // methods can be defined to write data into and to read data from one of the available cell
        public void SetOneCell(OrderClass newRequest)
        {
            lock (buffer)
            {
                // The number of cells available must be less than the max number of ticket brokers in your experiment
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == null)
                    {

                        buffer[i] = newRequest;

                        return;
                    }
                }
                semaphore.WaitOne();
            }
        }

        // write data into and to read data from one of the available cell
        public OrderClass GetOneCell(String sendId)
        {
            lock (buffer)
            {
                OrderClass request = new OrderClass();

                int Index = -1;

                // # cells available has to be be less than the max number of ticket broker in the experiment
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] != null)
                    {
                        request = buffer[i];

                        if (request.GetSenderId() == sendId)
                        {
                            Index = i;
                        }
                    }
                }

                if (Index != -1)
                {

                    buffer[Index] = null;
                }
                semaphore.Release();
                return request;
            }

        }
    }
}
