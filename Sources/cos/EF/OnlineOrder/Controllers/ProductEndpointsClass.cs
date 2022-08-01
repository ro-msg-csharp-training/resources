using Microsoft.EntityFrameworkCore;
using OnlineOrder.Data;
using OnlineOrder.Model;
namespace OnlineOrder.Controllers;

public static class ProductEndpointsClass
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Product", async (OnlineOrderContext db) =>
        {
            return await db.Product.ToListAsync();
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Product/{id}", async (int id, OnlineOrderContext db) =>
        {
            return await db.Product.FindAsync(id)
                is Product model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductByid")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Product/{id}", async (int id, Product product, OnlineOrderContext db) =>
        {
            var foundModel = await db.Product.FindAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            //update model properties here
            foundModel.Description = product.Description;
            foundModel.Name = product.Name;
            foundModel.UnitPrice = product.UnitPrice;
            foundModel.Weight = product.Weight; 

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Product/", async (Product product, OnlineOrderContext db) =>
        {
            db.Product.Add(product);
            await db.SaveChangesAsync();
            return Results.Created($"/Products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Product/{id}", async (int id, OnlineOrderContext db) =>
        {
            if (await db.Product.FindAsync(id) is Product product)
            {
                db.Product.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
