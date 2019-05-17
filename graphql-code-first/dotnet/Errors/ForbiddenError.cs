using System;
using System.Collections;
using dotnet.Errors;
using GraphQL;
using Microsoft.AspNetCore.Http;

namespace GettingStarted.Errors
{
    public class ForbiddenError : ExecutionError
    {
        public ForbiddenError(string message) : base(message)
        {
            Code = "FORBIDDEN";
            Data.Add(Constants.ExecutionErrorStatusCode, StatusCodes.Status403Forbidden);
        }

        protected ForbiddenError(string message, IDictionary data) : base(message, data)
        {
        }

        protected ForbiddenError(string message, Exception exception) : base(message, exception)
        {
        }
    }
}