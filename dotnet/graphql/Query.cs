using service.dataSources.authors;
using service.dataSources.books;
using Microsoft.AspNetCore.Http;

namespace service.graphql
{
    public partial class Query : IGraphQLOperation
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