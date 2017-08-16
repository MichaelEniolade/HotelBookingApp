using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework2
{
    class Program
    {
        public static Random rand = new Random();

        public static MultiCellBuffer mb = new MultiCellBuffer();

        public static HotelSupplier hs1 = new HotelSupplier(1, 10);
        public static HotelSupplier hs2 = new HotelSupplier(2, 20);
        
        public static TravelAgency ta1 = new TravelAgency(1, 5010123456789123);
        public static TravelAgency ta2 = new TravelAgency(2, 6010123456789123);
        public static TravelAgency ta3 = new TravelAgency(3, 6090123456789123);
        public static TravelAgency ta4 = new TravelAgency(4, 8320123456789123);
        
        static void Main(string[] args)
        {
            // PriceCutEvent subscriptions
            hs1.priceCutEvent += new HotelSupplier.priceCut(ta1.placeOrder);
            hs1.priceCutEvent += new HotelSupplier.priceCut(ta2.placeOrder);
            hs2.priceCutEvent += new HotelSupplier.priceCut(ta1.placeOrder);
            hs2.priceCutEvent += new HotelSupplier.priceCut(ta4.placeOrder);

            // makingOrderEvent subscriptions
            mb.makingOrderEvent += new MultiCellBuffer.orderDelegate(EncoderDecoder.retrieveOrder);

            // Each supplier and travel agency runs on their own thread to simulate multi processor system 
            Console.WriteLine("Starting hotel 1 thread");
            Thread hSup1 = new Thread(new ThreadStart(hs1.supplierFunc));
            hSup1.Start();

            Console.WriteLine("Starting hotel 2 thread");
            Thread hSup2 = new Thread(new ThreadStart(hs2.supplierFunc));
            hSup2.Start();

            Console.WriteLine("Starting agency 1 thread");
            Thread taThread1 = new Thread(new ThreadStart(ta1.agencyFunc));
            taThread1.Name = "T1";
            taThread1.Start();

            Console.WriteLine("Starting agency 2 thread");
            Thread taThread2 = new Thread(new ThreadStart(ta2.agencyFunc));
            taThread2.Name = "T2";
            taThread2.Start();

            Console.WriteLine("Starting agency 3 thread");
            Thread taThread3 = new Thread(new ThreadStart(ta3.agencyFunc));
            taThread3.Name = "T3";
            taThread3.Start();

            Console.WriteLine("Starting agency 4 thread");
            Thread taThread4 = new Thread(new ThreadStart(ta4.agencyFunc));
            taThread4.Name = "T4";
            taThread4.Start();

            hSup1.Join();
            hSup2.Join();

            taThread1.Join();
            taThread2.Join();
            taThread3.Join();
            taThread4.Join();

            Console.WriteLine("Process completed");
            Console.ReadKey();
        }
    }
}
