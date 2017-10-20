using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
								private IProductRepository repository;
								public int PageSize = 4;

								public ProductController(IProductRepository repo) {
												repository = repo;
								}

								public ViewResult List(string category,int page = 1) 
												=> View(new ProductListViewModel
												{
																// Brug denne privat metode til at differentiere mellem alle valgte og valgt på kategori:
																//Products = selectProducts((category == null), category)
																Products = repository.Products.Where(p => p.Category == null || p.Category == category)
																				.OrderBy(p => p.ProductID)
																				.Skip((page -1) * PageSize)
																				.Take(PageSize),
																PagingInfo = new PagingInfo
																{
																				CurrentPage = page,
																				ItemsPerpage = PageSize,
																				TotalItems = category == null ?
																								repository.Products.Count() :
																								repository.Products.Where(p => p.Category == category).Count()
																},
																CurrentCategory = category
												}
								);

								private IEnumerable<Product> selectProducts(bool all, string category)
								{
												if (all)
												{
																return repository.Products;
												}
												else
												{
																return repository.Products.Where(p => p.Category == category);
												}
								}
    }
}
