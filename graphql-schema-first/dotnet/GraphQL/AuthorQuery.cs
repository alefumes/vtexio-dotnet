using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Authors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    public partial class Query
    {
        [GraphQLMetadata("authors")]
        public IEnumerable<Author> GetAuthors(ResolveFieldContext context)
        {
            return authorsDataSource.GetAuthors()
            .Select(a => ConvertToGraphQLAuthor(a, ShouldIncludeBooks(context)));
        }

        [GraphQLMetadata("author")]
        public Author GetAuthor(ResolveFieldContext context, int id)
        {
            var author = authorsDataSource.GetAuthor(id);
            if (author == null)
            {
                return null;
            }
            
            return ConvertToGraphQLAuthor(author, ShouldIncludeBooks(context));
        }

        [GraphQLMetadata("authorsCount")]
        public int GetAuthorsCount()
        {
            return authorsDataSource.GetAuthors().Count;
        }

        [GraphQLMetadata("authorSource")]
        public string GetAuthorSource(ResolveFieldContext context, int id)
        {
            var author = GetAuthor(context, id);
            if (author == null)
            {
                return "could not find author";
            }

            return JsonConvert.SerializeObject(author);
        }

        private Author ConvertToGraphQLAuthor(Model.Author modelAuthor, bool includeBooks = false)
        {
            var modelBooks = includeBooks ? booksDataSource.GetBooksByAuthor(modelAuthor.Id) : null;
            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(modelAuthor, modelBooks);
        }

        private bool ShouldIncludeBooks(ResolveFieldContext context) => context.SubFields?.ContainsKey("books") ?? false;
    }
}