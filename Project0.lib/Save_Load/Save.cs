using System;
using DataBaseTargets.lib.Model;
using Managers;
using System.Linq;
using System.Collections.Generic;
using Objects;
using Microsoft.EntityFrameworkCore;

namespace SaveLoad
{
    public class Save
    {
        private static MainManager MyMainManager;
        private static PersonManager MyPersonManager;
        private static StoreManager MyStoreManager;
        private static OrderManager MyOrderManager;

        public void LoadAll(MainManager MyMainManager1, PersonManager MyPersonManager1, StoreManager MyStoreManager1, OrderManager MyOrderManager1)
        {
            MyMainManager = MyMainManager1;
            MyPersonManager = MyPersonManager1;
            MyStoreManager = MyStoreManager1;
            MyOrderManager = MyOrderManager1;
        }
        public void SaveAll()
        {
            SaveUsers();
            SaveStoreStock();
            SaveOrderStock();
        }

        private void SaveUsers()
        {
            Dictionary<string,Person> MyCurrentPeople = MyPersonManager.GetManagedPeople();
            using (PersonDbContext context = new PersonDbContext())
            {
                var People = context.People;

                foreach (var val in MyCurrentPeople)
                {
                    var x = context.People
                        .FirstOrDefault (Person => Person.Username == val.Key);
                    if(x == null)
                    {
                        People per  = new People()
                        {
                            PersonId = context.People.Max(Person => Person.PersonId)+1,
                            Username = val.Value.GetName(),
                            LocationId = context.Locations.First(Location => Location.LocationName == val.Value.GetLocation()).LocationId,
                            Password = val.Value.GetPassword(),
                            Employee = val.Value.GetEmployeeTag()
                        };

                        context.People.Add(per);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
        }

        private void SaveStoreStock()
        {
            Dictionary<string,Store> MyManagedStores = MyStoreManager.GetManagedStores();

            using (PersonDbContext context = new PersonDbContext())
            {
                var DataTopics = context.Topics;
                var DataStoreStock = context.StoreStock;
                var DataGeneralStock = context.GeneralStock
                    .Include(p => p.StoreStock);

                foreach (var val1 in MyManagedStores)
                {
                    Dictionary<string,List<Stock>> MyCurrentTopicStock = val1.Value.GetStock();
                    foreach (var val2 in MyCurrentTopicStock)
                    {
                        List<Stock> MyCurrentStock = val2.Value;
                        foreach (var val3 in MyCurrentStock)
                        {
                            var MyQuery = context.GeneralStock
                                .FirstOrDefault (Stock => Stock.StockName == val3.GetName());
                            
                            if (MyQuery == null)
                            {
                                int NewStockId;
                                int NewStoreStockId;

                                try
                                {
                                    NewStockId = context.GeneralStock.Max(stock => stock.StockId)+1;
                                }
                                catch (Exception)
                                {
                                    //null, no general stock exist
                                    NewStockId = 1;
                                }

                                try
                                {
                                    NewStoreStockId = context.StoreStock.Max(stock => stock.StoreStockId)+1;
                                }
                                catch (Exception)
                                {
                                    //null, no store stock exist
                                    NewStoreStockId = 1;
                                }
                                GeneralStock gensto = new GeneralStock
                                {
                                    StockId = NewStockId,
                                    TopicId = context.Topics.First(p => p.TopicName == val3.GetTopic()).TopicId,
                                    StockName = val3.GetName(),
                                    Price = Convert.ToDecimal(val3.GetPrice()),
                                    StockDescription = val3.GetDescription(),
                                    OrderStockId = null,
                                    StoreStockId = NewStoreStockId
                                };

                                StoreStock stosto = new StoreStock
                                {
                                    StoreStockId = NewStoreStockId,
                                    LocationId = context.Locations.First(p => p.LocationName == val1.Key).LocationId
                                };


                                context.StoreStock.Add(stosto);
                                context.GeneralStock.Add(gensto);
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SaveOrderStock()
        {
            Dictionary<string,List<Order>> MyManagedOrders = MyOrderManager.GetManagedOrders();

            using (PersonDbContext context = new PersonDbContext())
            {
                var DataTopics = context.Topics;
                var DataOrderStock = context.OrderStock;
                var DataGeneralStock = context.GeneralStock
                    .Include(p => p.OrderStock);
                var DataOrders = context.Orders;

                foreach (var val1 in MyManagedOrders)
                {
                    List<Order> MyCurrentNamedOrder = val1.Value;
                    foreach (var val2 in MyCurrentNamedOrder)
                    {
                        int added = 0;
                        List<Stock> MyCurrentStock = val2.GetItems();
                        foreach (var val3 in MyCurrentStock)
                        {
                            var MyQuery = context.GeneralStock
                                .FirstOrDefault (Stock => Stock.StockName == val3.GetName());
                            
                            if (MyQuery == null)
                            {
                                GeneralStock gensto = new GeneralStock
                                {
                                    StockId = context.GeneralStock.Max(stock => stock.StockId)+1,
                                    TopicId = context.Topics.First(p => p.TopicName == val3.GetTopic()).TopicId,
                                    StockName = val3.GetName(),
                                    Price = Convert.ToDecimal(val3.GetPrice()),
                                    StockDescription = val3.GetDescription(),
                                    OrderStockId = context.OrderStock.Max(stock => stock.OrderStockId)+1,
                                    StoreStockId = null
                                };

                                Orders ords = new Orders()
                                {
                                    OrderId = context.Orders.Max(o => o.OrderId)+1+added,
                                    PersonId = context.People.First(p => p.Username == val1.Key).PersonId,
                                    Price = Convert.ToDecimal(val2.GetPrice()), 
                                    OrderDate = val2.GetDate()
                                };

                                OrderStock ordsto = new OrderStock
                                {
                                    OrderStockId = (int)gensto.OrderStockId,
                                    OrderId = ords.OrderId
                                };

                                context.Orders.Add(ords);
                                context.GeneralStock.Add(gensto);
                                context.OrderStock.Add(ordsto);
                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SwapToOrder(string x)
        {
            using (PersonDbContext context = new PersonDbContext())
            {
                var DataGeneralStock = context.GeneralStock
                    .FirstOrDefault(p => p.StockName == x);
                var DataOrders = context.Orders
                    .ToList();
                var DataPeople = context.People
                    .ToList();

                if (DataGeneralStock == null)
                {
                    Console.WriteLine("Error! Item Not found!");
                }
                else
                {
                    Orders ords;
                    int ordsOrderId;
                    try
                    {
                        ordsOrderId = context.Orders.Max(o => o.OrderId)+1;
                    }
                    catch (Exception)
                    {
                        ordsOrderId = 1;
                    }
                    if (MyOrderManager.GetCurrentOrder().GetItems().Count <= 1)
                    {
                        ords = new Orders()
                        {
                            OrderId = ordsOrderId,
                            PersonId = context.People.First(p => p.Username == MyPersonManager.GetCurrentUser().GetName()).PersonId,
                            Price = 0, 
                            OrderDate = DateTime.Now
                        };
                        context.Orders.Add(ords);
                    }
                    else
                    {
                        var w = context.People.First(p => p.Username == MyPersonManager.GetCurrentUser().GetName()).PersonId;
                        var e = context.Orders.Max(p => p.OrderId);
                        ords = context.Orders.First(p => p.OrderId == e);
                    }
                    var count = (from orsto in context.OrderStock select orsto.OrderId).Count();
                    if (count == 0)
                    {
                        var q = context.StoreStock
                            .First(p => p.StoreStockId == DataGeneralStock.StoreStockId);
                        context.StoreStock.Remove(q);
                        DataGeneralStock.StoreStockId = null;
                        OrderStock ordsto = new OrderStock
                        {
                            OrderStockId = 1,
                            OrderId = ords.OrderId
                        };
                        DataGeneralStock.OrderStockId = ordsto.OrderStockId;
                        context.OrderStock.Add(ordsto);
                    }
                    else
                    {
                        var q = context.StoreStock
                            .First(p => p.StoreStockId == DataGeneralStock.StoreStockId);
                        context.StoreStock.Remove(q);
                        DataGeneralStock.StoreStockId = null;
                        OrderStock ordsto = new OrderStock
                        {
                            OrderStockId = context.OrderStock.Max(stock => stock.OrderStockId)+1,
                            OrderId = ords.OrderId
                        };
                        DataGeneralStock.OrderStockId = ordsto.OrderStockId;
                        context.OrderStock.Add(ordsto);
                        DataGeneralStock.OrderStockId = context.OrderStock.Max(order => order.OrderStockId)+1;
                    }
                    double totalprice = 0;
                    foreach (var val in MyOrderManager.GetCurrentOrder().GetItems())
                    {
                        totalprice = totalprice + val.GetPrice();
                    }

                    ords.Price = Convert.ToDecimal(totalprice);
                    //context.OrderStock.Add(ords);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

            }
        }

        public void SwapToStore(string x)
        {
            using (PersonDbContext context = new PersonDbContext())
            {
                var DataGeneralStock = context.GeneralStock
                    .FirstOrDefault(p => p.StockName == x);
                var DataLocation = context.Locations
                    .ToList();
                var DataPeople = context.People
                    .ToList();

                if (DataGeneralStock == null)
                {
                    Console.WriteLine("Error! Item Not found!");
                    Console.ReadLine();
                }
                else
                {
                    var count = (from storsto in context.StoreStock select storsto.StoreStockId).Count();
                    if (count == 0)
                    {
                        var q = context.OrderStock
                            .First(p => p.OrderStockId == DataGeneralStock.OrderStockId);
                        context.OrderStock.Remove(q);
                        DataGeneralStock.OrderStockId = null;
                        var e = DataPeople
                            .First(p => p.Username == MyPersonManager.GetCurrentUser().GetLocation()).LocationId;
                        var w = DataLocation
                            .First(p => p.LocationId == e);
                        StoreStock storsto = new StoreStock
                        {
                            StoreStockId = 1,
                            LocationId = w.LocationId
                        };
                        DataGeneralStock.StoreStockId = storsto.StoreStockId;
                        context.StoreStock.Add(storsto);
                    }
                    else
                    {
                        var q = context.OrderStock
                            .First(p => p.OrderStockId == DataGeneralStock.OrderStockId);
                        context.OrderStock.Remove(q);
                        DataGeneralStock.OrderStockId = null;
                        var e = DataLocation
                            .First(p => p.LocationName == MyPersonManager.GetCurrentUser().GetLocation()).LocationId;
                        var w = DataPeople
                            .First(p => p.LocationId == e);
                        StoreStock storsto = new StoreStock
                        {
                            StoreStockId = context.StoreStock.Max(stock => stock.StoreStockId)+1,
                            LocationId = w.LocationId
                        };
                        DataGeneralStock.StoreStockId = storsto.StoreStockId;
                        context.StoreStock.Add(storsto);
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

            }
        }

        public bool CreateLocation(string MyNewLocation)
        {
            using (PersonDbContext context = new PersonDbContext())
            {
                var DataLocations = context.Locations;

                var ExistsQuary = DataLocations.FirstOrDefault(p => p.LocationName == MyNewLocation);

                if (ExistsQuary == null)
                {
                    Locations MyNewLocationData = new Locations()
                    {
                        LocationId = DataLocations.Max(p => p.LocationId)+1,
                        LocationName = MyNewLocation
                    };
                    DataLocations.Add(MyNewLocationData);
                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                return false;
            }
        }
    }
}