using service.dataSources.authors;
using service.dataSources.books;
using Microsoft.AspNetCore.Http;
using GraphQL;

namespace service.graphql
{
    [GraphQLMetadata("Query")]
    public partial class Query
    {
        private readonly IBooksDataSource booksDataSource;
        private readonly IAuthorsDataSource authorsDataSource;
        private readonly IHttpContextAccessor context;
        public Query(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource, IHttpContextAccessor context)
        {
            this.context = context;
            this.authorsDataSource = authorsDataSource;
            this.booksDataSource = booksDataSource;
        }
    }
}