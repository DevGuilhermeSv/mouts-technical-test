﻿using Ambev.DeveloperEvaluation.Application.Products;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Profile for mapping between Cart entity and CreateCartResponse
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart operation
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>()
            .ConstructUsing(cmd =>
                new Cart(
                     cmd.UserId,
                     cmd.Date,
                     cmd.Products
                        .Select(p => new CartProduct(p.ProductId, p.Quantity,null))
                        .ToList()
                ));
        CreateMap<Cart, CreateCartResult>();
        CreateMap<CartProductDto, CartProduct>().ReverseMap();
    }
}
