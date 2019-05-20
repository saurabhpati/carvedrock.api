﻿using CarvedRock.Repositories.Models;
using CarvedRockApi.Repositories;
using GraphQL.Types;

namespace CarvedRockApi.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        public CarvedRockQuery(IRepository<Product, int> productRepository)
        {
            Field<ListGraphType<GraphQL.Types.ProductType>>("products", resolve: context => productRepository.GetAllAsync());

            Field<GraphQL.Types.ProductType>(
                "product",
                null,
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>() { Name = "id" }), 
                    resolve: context => productRepository.GetAsync(context.GetArgument<int>("id")));
        }
    }
}
