using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework2
{
    class HotelSupplier
    {
        public delegate void priceCut();
        public event priceCut priceCutEvent;

        int hotelId, capacity;
        int numPriceCuts = 2;
        private int hotelPrice = Program.rand.Next(50, 200);

        public HotelSupplier(int id, int capacity) {
            this.hotelId = id;
            this.capacity = capacity;
        }

        public void supplierFunc() {
            for (int i = 0; i < numPriceCuts; i++)
            {
                Thread.Sleep(1000);
                PricingModel();
            }
        }

        public void PricingModel() {
            //Console.WriteLine("Inside pricing model method");
            int price = Program.rand.Next(50, 200);

            if (price < hotelPrice)
            {
                Console.WriteLine(string.Format("Decreased the price of hotel of {0} from {1} to {2}",
                                                hotelId, hotelPrice, price));
                hotelPrice = price;
                Console.WriteLine("Calling eventhandler");
                priceCutEvent?.Invoke();
            }
            else {
                hotelPrice = price;
            }
        }

        public String ProcessOrder(OrderObject order) {
            bool isValidCardno = false;
            int numRooms = order.getAmount();

            Console.WriteLine("Checking card validity");
            // Check validity of cardNo
            long cardNo = order.getCardNo();
            if (cardNo > 5000000000000000 && cardNo < 7000000000000000) {
                isValidCardno = true;
            }

            if (isValidCardno)
            {
                Console.WriteLine("\tCardNo is valid");
                if (numRooms <= this.capacity)
                {
                    this.capacity -= numRooms;
                    Console.WriteLine(String.Format("\tCost per room = {0}", hotelPrice));
                    // Time between order placed and order processed
                    TimeSpan timeToProcessOrder = DateTime.Now - order.getOrderPlacedTime();
                    return String.Format("ORDER CONFIRMED - Total amount = {0}, order process time = {1} milliseconds",
                                            numRooms * hotelPrice, timeToProcessOrder.Milliseconds);
                }
                else
                {       
                    return "ORDER DECLINED - There are no sufficient rooms in this hotel";
                }
            }
            else
            {
                return "ORDER DECLINED - Card is invalid";
            }
        }
    }

}
