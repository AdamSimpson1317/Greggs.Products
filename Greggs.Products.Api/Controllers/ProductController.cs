using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    /*private static readonly string[] Products = new[]
    {
        "Sausage Roll", "Vegan Sausage Roll", "Steak Bake", "Yum Yum", "Pink Jammie"
    };*/

    private readonly ILogger<ProductController> _logger;

    private readonly IDataAccess<Product> _product;

    private readonly decimal _euroConversion = 1.11m;


    public ProductController(ILogger<ProductController> logger, IDataAccess<Product> product)
    {
        _logger = logger;
        _product = product;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
    {

        var products = _product.List(pageStart, pageSize);

        foreach(var prod in products)
        {
            prod.PriceInEuros = prod.PriceInPounds * _euroConversion;
        }

        /*if (pageSize > Products.Length)
            pageSize = Products.Length;
        

        var rng = new Random();
        return Enumerable.Range(1, pageSize).Select(index => new Product
            {
                PriceInPounds = rng.Next(0, 10),
                Name = Products[rng.Next(Products.Length)]
            })
            .ToArray();
        */  
        return products;
    }
}