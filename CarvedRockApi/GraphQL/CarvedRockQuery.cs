using CarvedRock.Api.Data.Entities;
using CarvedRockApi.Repositories;
using GraphQL.Types;

namespace CarvedRockApi.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType  
    {
        public CarvedRockQuery(IRepository<Product, int> productRepository)
        {
            Field<ListGraphType<GraphQL.Types.ProductType>>("products", resolve: context => productRepository.GetAllAsync());
        }
    }
}
