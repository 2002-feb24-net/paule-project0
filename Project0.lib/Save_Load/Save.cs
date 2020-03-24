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
        static MainManager MyMainManager;
        static PersonManager MyPersonManager;
        static StoreManager MyStoreManager;
        static OrderManager MyOrderManager;
        public void SaveAll(MainManager MyMainManager1, PersonManager MyPersonManager1, StoreManager MyStoreManager1, OrderManager MyOrderManager1)
        {
            MyMainManager = MyMainManager1;
            MyPersonManager = MyPersonManager1;
            MyStoreManager = MyStoreManager1;
            MyOrderManager = MyOrderManager1;
            SaveUsers();
            SaveStoreStock();
        }

        public void SaveUsers()
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

        public void SaveStoreStock()
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
                            var MyQuerry = context.GeneralStock
                                .FirstOrDefault (Stock => Stock.StockName == val3.GetName());
                            
                            if (MyQuerry == null)
                            {
                                GeneralStock gensto = new GeneralStock
                                {
                                    StockId = context.GeneralStock.Max(stock => stock.StockId)+1,
                                    TopicId = context.Topics.First(p => p.TopicName == val3.GetTopic()).TopicId,
                                    StockName = val3.GetName(),
                                    Price = Convert.ToDecimal(val3.GetPrice()),
                                    StockDescription = val3.GetDescription(),
                                    OrderStockId = null,
                                    StoreStockId = context.StoreStock.Max(stock => stock.StoreStockId)+1
                                };

                                StoreStock stosto = new StoreStock
                                {
                                    StoreStockId = (int)gensto.StoreStockId,
                                    LocationId = context.Locations.First(p => p.LocationName == val2.Key).LocationId
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

        public void SaveOrderStock()
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
                        List<Stock> MyCurrentStock = val2.GetItems();
                        foreach (var val3 in MyCurrentStock)
                        {
                            var MyQuerry = context.GeneralStock
                                .FirstOrDefault (Stock => Stock.StockName == val3.GetName());
                            
                            if (MyQuerry == null)
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
                                    OrderId = context.Orders.Max(o => o.OrderId)+1,
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
                    var count = (from orsto in context.OrderStock select orsto.OrderId).Count();
                    if (count == 0)
                    {
                        var q = context.StoreStock
                            .First(p => p.StoreStockId == DataGeneralStock.StoreStockId);
                        context.StoreStock.Remove(q);
                        DataGeneralStock.StoreStockId = null;
                        Console.WriteLine(MyPersonManager);
                        var e = DataPeople
                            .First(p => p.Username == MyPersonManager.GetCurrentUser().GetName()).PersonId;
                        var w = DataOrders
                            .First(p => p.PersonId == e);
                        OrderStock ordsto = new OrderStock
                        {
                            OrderStockId = context.OrderStock.Max(stock => stock.OrderStockId)+1,
                            OrderId = w.OrderId
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
                        DataGeneralStock.OrderStockId = context.OrderStock.Max(order => order.OrderStockId)+1;
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

        public void SwapToStore(string x)
        {
            using (PersonDbContext context = new PersonDbContext())
            {
                var DataGeneralStock = context.GeneralStock
                    .FirstOrDefault(p => p.StockName == x);

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
                        DataGeneralStock.StoreStockId = 1;
                        DataGeneralStock.OrderStockId = null;
                    }
                    else
                    {
                        DataGeneralStock.StoreStockId = context.StoreStock.Max(stock => stock.StoreStockId)+1;
                        DataGeneralStock.OrderStockId = null;
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
    }
}