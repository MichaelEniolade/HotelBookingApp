using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework2
{
    class EncoderDecoder
    {
        // Convert the object into a string
        public static string Encode(OrderObject orderObj)
        {
            return String.Format("{0}|{1}|{2}|{3}|{4}", orderObj.getSenderId(), orderObj.getSupplierId(), orderObj.getCardNo(), orderObj.getAmount(), orderObj.getOrderPlacedTime());
        }

        // Convert the string back to object
        public static OrderObject Decode(String encodedString)
        {
            OrderObject decodedOrder = new OrderObject();
            String[] strArr = encodedString.Split('|');
            decodedOrder.setSenderId(Convert.ToInt32(strArr[0]));
            decodedOrder.setSupplierId(Convert.ToInt32(strArr[1]));
            decodedOrder.setCardNo((long)Convert.ToDouble(strArr[2]));
            decodedOrder.setAmount(Convert.ToInt32(strArr[3]));
            decodedOrder.setOrderPlacedTime(Convert.ToDateTime(strArr[4]));
            return decodedOrder;
        }

        // Event handler for makingOrder event
        // This method is executed when an orderObj is inserted into the multicell buffer
        public static void retrieveOrder() {
            String encodedOrder;
            Monitor.Enter(Program.mb);
            try
            {
                encodedOrder = Program.mb.getOneCell();
            }
            finally {
                Monitor.Exit(Program.mb);
            }

            OrderObject decodedOrder = Decode(encodedOrder);
            Console.WriteLine(String.Format("\nNew order received -> From travel agency {0} to hotel supplier {1} for {2} rooms with card no. {3}",
                                            decodedOrder.getSenderId(), decodedOrder.getSupplierId(), decodedOrder.getAmount(), decodedOrder.getCardNo()));
            int supplierId = decodedOrder.getSupplierId();

            if (supplierId == 1)
            {
                Console.WriteLine("Hotel 1 is processing the order");
                Console.WriteLine(Program.hs1.ProcessOrder(decodedOrder));
            }
            if (supplierId == 2)
            {
                Console.WriteLine("Hotel 2 is processing the order");
                Console.WriteLine(Program.hs2.ProcessOrder(decodedOrder));
            }
        }
    }
}
