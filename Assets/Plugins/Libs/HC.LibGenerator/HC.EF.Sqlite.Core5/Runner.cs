using System;
using System.Linq;
using HC.Data;
using Microsoft.EntityFrameworkCore;

namespace HC.EF.Sqlite.Core5
{
    public class Runner
    {
        public void Run()
        {
            try
            {
                var newStorageId = 0;

                //Create
                using (var db = new TestContext())
                {
                    Console.WriteLine("Inserting a new data");
                    var storage = new Storage { Name = "TestStorage" };
                    //db.Add(storage);
                    db.Storages.Add(storage);
                    db.SaveChanges();
                    newStorageId = storage.Id;
                }

                //Read
                using (var db = new TestContext())
                {
                    var storage = db.Storages.Find(newStorageId);

                    db.Products.Add(new Product() { Name = "P1", StorageId = storage.Id });
                    db.Products.Add(new Product() { Name = "P2", Storage = storage });
                    db.SaveChanges();
                }

                // //Update
                using (var db = new TestContext())
                {
                    // Read
                    var storage = db.Storages
                        .Include(x => x.Products)
                        .FirstOrDefault(x => x.Id == newStorageId);
                    
                    var products = storage.Products.ToList();

                    foreach (var product in products)
                    {
                        product.Name = "UpdatedName";
                    }

                    db.SaveChanges();
                }

                using (var db = new TestContext())
                {
                    foreach (var product in db.Products)
                    {
                        Console.WriteLine($"{product.Name}\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}