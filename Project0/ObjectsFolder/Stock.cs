using System;
using System.Collections.Generic;

namespace Objects
{
    class Stock : ObjectParent
    {
        private string topic;
        private string name;
        private double price;
        private string description;

        public void SetName(string x)
        {
            this.name = x;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetPrice(double x)
        {
            this.price = x;
        }

        public double GetPrice()
        {
            return this.price;
        }

        public void SetDescription(string x)
        {
            this.description = x;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public void SetTopic(string x)
        {
            this.topic = x;
        }

        public string GetTopic()
        {
            return this.topic;
        }
    }
}