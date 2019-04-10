using GettingStarted.DataSources.Authors;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using GraphQL;

namespace GettingStarted.GraphQL
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