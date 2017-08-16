using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework2
{
    class OrderObject
    {
        private int senderId, supplierId, amount;
        private long cardNo;
        private DateTime orderPlacedTime = DateTime.Now;

        public OrderObject()
        {
        }

        public OrderObject(int senderId, int supplierId, long cardNo, int amount)
        {
            this.senderId = senderId;
            this.supplierId = supplierId;
            this.cardNo = cardNo;
            this.amount = amount;
            orderPlacedTime = DateTime.Now;
        }
        
        public void setSenderId(int senderId) {
            this.senderId = senderId;
        }

        public int getSenderId() {
            return this.senderId;
        }

        public void setSupplierId(int receiverId)
        {
            this.supplierId = receiverId;
        }

        public int getSupplierId()
        {
            return this.supplierId;
        }
        public void setAmount(int amount)
        {
            this.amount = amount;
        }

        public int getAmount()
        {
            return this.amount;
        }

        public void setCardNo(long cardNo)
        {
            this.cardNo = cardNo;
        }

        public long getCardNo()
        {
            return this.cardNo;
        }

        public void setOrderPlacedTime(DateTime time)
        {
            this.orderPlacedTime = time;
        }

        public DateTime getOrderPlacedTime()
        {
            return this.orderPlacedTime;
        }
    }
}
