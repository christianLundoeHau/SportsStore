using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
								private List<CartLine> lineCollection = new List<CartLine>();

								public virtual void AddItem(Product product, int quantity)
								{
												CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
												if(line != null)
												{
																line.Quantity += quantity;
												}
												else
												{
																lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
												}
								}

								public virtual void RemoveLine(Product product) => 
												lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

								public virtual int ComputeTotalItems() => lineCollection.Sum(i => i.Quantity);

								public virtual decimal ComputeTotalPrice() =>
												lineCollection.Sum(v => v.Product.Price * v.Quantity);

								public virtual void Clear() =>
												lineCollection.Clear();

								public virtual IEnumerable<CartLine> GetLines => lineCollection;
    }

				public class CartLine
				{
								public int CartLineID { get; set; }
								public Product Product { get; set; }
								public int Quantity { get; set; }
				}
}
