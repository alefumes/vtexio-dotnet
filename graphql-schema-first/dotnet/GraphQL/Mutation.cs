using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using GettingStarted.Utils;
using GettingStarted.DataSources.Authors;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Mutation")]
    public partial class Mutation
    {
        private readonly IBooksDataSource booksDataSource;
        private readonly IAuthorsDataSource authorsDataSource;
        private readonly IHttpContextAccessor contextAccessor;
        public Mutation(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.authorsDataSource = authorsDataSource;
            this.booksDataSource = booksDataSource;
        }
    }
}