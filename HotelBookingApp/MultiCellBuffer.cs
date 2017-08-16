using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace homework2
{
    class MultiCellBuffer
    {
        // As soon as the order is placed in multicell buffer using setOneCell, an event is triggered to execute makingOrder event handler
        public delegate void orderDelegate();
        public event orderDelegate makingOrderEvent;

        // Storing the orders in a circular array of size 3
        private const int SIZE = 3;
        int head, tail;
        int currSize;
        
        private String[] bufferArr = new String[SIZE];

        // Maximum number of reads are limited to 2 (no. of hotel suppliers) and max writes limited to 3 (capacity of the array)
        Semaphore readSem = new Semaphore(2, 2);
        Semaphore writeSem = new Semaphore(3, 3);
        
        public MultiCellBuffer() {
            head = 0;
            tail = 0;
            currSize = 0;
        }

        public String getOneCell()
        {
            readSem.WaitOne();
            lock (this)
            {
                while (currSize == 0)
                {
                    Monitor.Wait(this);
                }

                String currOrder = bufferArr[head];
                head = (head + 1) % SIZE;
                currSize--;
                readSem.Release();
                Monitor.Pulse(this);
                return currOrder;
            }
        }

        public void setOneCell(String order) {
            writeSem.WaitOne();
            lock (this)
            {
                while (currSize == SIZE)
                {
                    Monitor.Wait(this);
                }

                bufferArr[tail] = order;
                tail = (tail + 1) % SIZE;
                currSize++;
                writeSem.Release();
                // Emitting the event
                makingOrderEvent();

                Monitor.Pulse(this);
            }
        }
    }
}
