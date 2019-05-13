using System;
using System.Collections;
using dotnet.Errors;
using GraphQL;
using Microsoft.AspNetCore.Http;

namespace GettingStarted.Errors
{
    public class ResolverError : ExecutionError
    {
        public ResolverError(string message) : base(message)
        {
            Code = "RESOLVER_ERROR";
            Data.Add(Constants.ExecutionErrorStatusCode, StatusCodes.Status500InternalServerError);
        }

        protected ResolverError(string message, IDictionary data) : base(message, data)
        {
        }

        protected ResolverError(string message, Exception exception) : base(message, exception)
        {
        }
    }
}