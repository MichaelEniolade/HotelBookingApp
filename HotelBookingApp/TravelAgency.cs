using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework2
{
    class TravelAgency
    {
        int agencyId, numRooms;
        long cardNo;
        string encodedOrder = null;

        public TravelAgency(int id, long cardNo)
        {
            this.agencyId = id;
            this.cardNo = cardNo;
            this.numRooms = 1;
            this.encodedOrder = null;
        }

        public void placeOrder()
        {
            numRooms = Program.rand.Next(1, 10);
            int supplierId = Program.rand.Next(1, 3);
            OrderObject orderObj = new OrderObject(agencyId, supplierId, cardNo, numRooms);
            String encodedOrder = EncoderDecoder.Encode(orderObj);
            
            // Pushing the order into multicellbuffer
            Monitor.Enter(Program.mb);
            try
            {
                //Console.WriteLine("Thread " + Thread.CurrentThread.Name + " Setting Order");
                Program.mb.setOneCell(encodedOrder);
            }
            finally { Monitor.Exit(Program.mb); }
        }
        
        public void agencyFunc()
        {
            for (int i = 0; i < 2; i++)
            {
                placeOrder();
                Thread.Sleep(500);
            }
        }
    }
}
