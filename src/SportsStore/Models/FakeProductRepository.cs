using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
				public class FakeProductRepository : IProductRepository
				{
								public IEnumerable<Product> Products => new List<Product> {
												new Product {Name ="Foorball", Price=25 , Category="Soccer"},
												new Product {Name="Surf board", Price=179, Category="Watersports" },
												new Product {Name="Running shoes", Price=95, Category="Chess"}
								};
				}
}
