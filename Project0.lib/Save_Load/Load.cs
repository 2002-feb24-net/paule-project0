using System;
using DataBaseTargets.lib.Model;
using Managers;
using System.Linq;
using System.Collections.Generic;
using Objects;
using Microsoft.EntityFrameworkCore;

namespace SaveLoad
{
    public class Load
    {
        private PersonManager MyPersonManager;
        private StoreManager MyStoreManager;
        private OrderManager MyOrderManager;
        public void LoadAllInfo(PersonManager MyPersonManager,StoreManager MyStoreManager,OrderManager MyOrderManager)
        {
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
            
            MyPersonManager.PullData();
            MyStoreManager.PullData();
            MyOrderManager.PullData();


        }

        public Dictionary<string,List<Order>> PullOrderDictionary()
        {
            Dictionary<string,List<Order>> MyManagedOrders = new Dictionary<string,List<Order>>();

            List<Order> MyOrderList = PullOrderList();

            foreach (Order val in MyOrderList)
            {
                Console.WriteLine($"Order was sold on {val.GetDate()} to {val.GetName()} for {val.GetPrice()}.");
            }

            foreach (Order val in MyOrderList)
            {
                if (MyManagedOrders.ContainsKey(val.GetName()))
                {
                    MyManagedOrders[val.GetName()].Add(val);
                }
                else
                {
                    List<Order> MyOrders = new List<Order>();
                    MyManagedOrders.Add(val.GetName(),MyOrders);
                    MyManagedOrders[val.GetName()].Add(val);
                }
            }

            return MyManagedOrders;

        }

        public List<Order> PullOrderList()
        {
            List<Order> MyCurrentOrderList = new List<Order>();
            using (var context = new PersonDbContext())
            {
                List<Orders> MyOrders = context.Orders
                    .Include(p => p.Person)
                    .ToList();
                List<GeneralStock> MyGeneralStock = context.GeneralStock
                    .Include(p => p.OrderStock)
                    .Include(p => p.OrderStock.Order)
                    .Include(p => p.Topic)
                    .ToList();
                
                foreach (Orders val1 in MyOrders)
                {
                    Order MyCurrentOrder = new Order();
                    List<Stock> MyCurrentStockList = new List<Stock> ();

                    foreach (GeneralStock val2 in MyGeneralStock)
                    {
                        if(val2.OrderStock == null)
                        {
                            continue;
                        }
                        if (val2.OrderStock.OrderId == val1.OrderId)
                        {
                            Stock MyCurrentStock = new Stock();
                            MyCurrentStock.SetDescription(val2.StockDescription);
                            MyCurrentStock.SetName(val2.StockName);
                            MyCurrentStock.SetPrice(Convert.ToDouble(val2.Price));
                            MyCurrentStock.SetTopic(val2.Topic.TopicName);
                            MyCurrentStockList.Add(MyCurrentStock);
                        }
                    }

                    if (MyCurrentStockList.Count > 0)
                    {
                        MyCurrentOrder.SetItems(MyCurrentStockList);
                        MyCurrentOrder.SetDate(val1.OrderDate);
                        MyCurrentOrder.SetName(val1.Person.Username);
                        MyCurrentOrderList.Add(MyCurrentOrder);
                    }
                }
            }
            return MyCurrentOrderList;
        }

        public Dictionary<string,Person> PullPeopleDictionary()
        {
            Dictionary<string,Person> MyPersonDictionary = new Dictionary<string,Person>();

            using (var context = new PersonDbContext())
            {
                List<People> MyPeople = context.People
                    .ToList();
                List<Locations> MyLocations = context.Locations
                    .ToList();
                
                foreach (var val in MyPeople)
                {
                    Person MyPerson = new Person();
                    MyPerson.SetPassword(val.Password);
                    MyPerson.SetName(val.Username);
                    MyPerson.SetEmployeeTag(val.Employee);
                    var MyLocationQuary = context.Locations
                        .First (Location => Location.LocationId == val.LocationId).LocationName;
                        
                    MyPerson.SetLocation(MyLocationQuary);

                    MyPersonDictionary.Add(MyPerson.GetName(),MyPerson);
                }
                return MyPersonDictionary;
            }
        }
        public Dictionary<string,Store> PullStoreDictionary()
        {
            Dictionary<string,Store> MyStoreDictionary = new Dictionary<string,Store>();

            using (var context = new PersonDbContext())
            {
                List<GeneralStock> MyGeneralStock = context.GeneralStock
                    .Include(p => p.StoreStock)
                    .Include(p => p.StoreStock.Location)
                    .Include(p => p.Topic)
                    .ToList();
                List<Locations> MyLocations = context.Locations
                    .ToList();
                foreach (var val1 in MyLocations)
                {
                    List<Stock> MyCurrentStockList = new List<Stock>();
                    foreach (var val2 in MyGeneralStock)
                    {
                        if(val2.StoreStock == null)
                        {
                            continue;
                        }
                        if (val2.StoreStock.LocationId == val1.LocationId)
                        {
                            Stock MyCurrentStock = new Stock();
                            MyCurrentStock.SetDescription(val2.StockDescription);
                            MyCurrentStock.SetName(val2.StockName);
                            MyCurrentStock.SetPrice(Convert.ToDouble(val2.Price));
                            MyCurrentStock.SetTopic(val2.Topic.TopicName);
                            MyCurrentStockList.Add(MyCurrentStock);
                        }
                    }

                    if (MyStoreDictionary.ContainsKey(val1.LocationName))
                    {
                        foreach (var val in MyCurrentStockList)
                        {
                            MyStoreDictionary[val1.LocationName].AddStock(val);
                        }
                    }
                    else
                    {
                        Store MyNewStore = new Store();
                        MyNewStore.SetName(val1.LocationName);
                        Dictionary<string,List<Stock>> SpecificStoreStock = new Dictionary<string,List<Stock>>();
                        MyNewStore.SetMyStock(SpecificStoreStock);
                        MyStoreDictionary.Add(val1.LocationName,MyNewStore);
                        foreach (var val in MyCurrentStockList)
                        {
                            MyStoreDictionary[val1.LocationName].AddStock(val);
                        }
                    }
                }
            }

            return MyStoreDictionary;
        }
        
        public List<string> PullLocationNames()
        {
            List<string> MyCurrentLocations = new List<string>();
            using (var context = new PersonDbContext())
            {
                List<Locations> MyLocations = context.Locations
                    .ToList();
                
                foreach (var val in MyLocations)
                {
                    MyCurrentLocations.Add(val.LocationName);
                }
            }
            return MyCurrentLocations;
        }
    }
}