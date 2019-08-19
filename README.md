# .Net Getting Started

A quick start app using the `dotnet` builder for building backend apps.

For more information, please check our [developer docs](https://vtex.io).

## Getting Started

First, install the VTEX Toolbelt and login:

```bash
$ npm i -g vtex
$ vtex login
```

Now, download this repo and open a terminal in the `rest-api` folder.

To start coding you need a workspace (it's like a `git` branch of your store). Let's create a workspace named `dev`:

```bash
$ vtex use dev
```

Then, use `link` to start developing your app:

```bash
$ vtex link
```

Finally, access your public endpoint at:

`https://{{workspace}}--{{account}}.myvtex.com/_v/vtex/hello`

You can also access private endpoints, but you need to send your token in the `Authorization` header.

VTEX Toolbelt will copy your token to the clipboard if you run the following command:

```bash
$ vtex local token
```

Now that you already have a token you can call your private endpoint:

`http://dotnet-rest-api.vtex.aws-us-east-1.vtex.io/{{account}}/{{workspace}}/_v/vtex/private`

## Setting up my application

Your application details (name, version and description, for instance) must be set at your `manifest.json` file.

```json
{
  "name": "Calk",
  "vendor": "mycompany",
  "version": "1.3.18",
  "title": "Smart Scientific Calculator",
  "description": "A smart scientific calculator API with financial tools",
  ...
}
```

## Adding my own code

To add your own methods you need to define your routes at `dotnet/service.json`.

```json
...
"routes": {
  "myPublicRoute": {
    "path": "/my-public-route",
    "public": true
  },
  "myPrivateRoute": {
    "path": "/my-private-route",
    "public": false
  }
}
...
```

Then you just need to open the `dotnet/Controllers/RoutesController.cs` and write methods with the same names you gave to your routes.

```C#
public class RoutesController : Controller
{
    public string MyPublicRoute()
    {
        return "This is my public route response";
    }

    public string MyPrivateRoute()
    {
        return "This is my private route response";
    }
}
```

If you need advanced application configuration, you can use the `StartupExtender` class to configure the services you need and change the request pipeline.

## Routing

When you define a route you need to specify if it's _public_ or _private_.

A public route can be called without an authorization token through the following URL pattern:

`https://{{workspace}}--{{account}}.myvtex.com/{{yourPath}}`

A private route needs an authorization token (that should be sent in the `Authorization` header) and can be called with the following pattern:

`http://{{appName}}.{{vendor}}.{{region}}.vtex.io/{{account}}/{{workspace}}/{{yourPath}}`

**Important:** Note that you can't declare routes that start with `/api`. This is a reserved path segment.

### Named Parameters

You can define named path parameters for your routes by adding a `:` before its names. The following example shows a route with a `code` parameter.

```json
...
"routes": {
  "cars": {
    "path": "/_v/cars/:code",
    "public": true
  }
}
...
```

### Catch-all Parameters

The router will compare the route patterns in the order they are defined, so you need to make sure you set the more specific ones first.

If you need to define catch-all routes to handle requests that can't match your specific routes, you can do it by using an `*` before the name of the parameter.

In the example below you have a catch-all route that handles all requests that start with `/_v` (`apiDefault`) and a more generic one to handle any request that couldn't match any previous pattern (`default`).

```json
...
"routes": {
  ...
  "apiDefault": {
    "path": "/_v/*url",
     "public": true
  },
  "default": {
    "path": "/*url",
    "public": true
  }
}
...
```

### Outbound Requests

If you want to send outbound requests you need to specify the domains in the `outbound-access` policy in your `manifest.json` file.

```json
"policies": [
    {
      "name": "outbound-access",
      "attrs": {
        "host": "bnb.data.bl.uk"
      }
    }
  ]
```

You also need to send an authorization token in the `Proxy-Authorization` header.

```C#
string authToken = HttpContext.Request.Headers["X-Vtex-Credential"];

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("http://bnb.data.bl.uk")
};

request.Headers.Add("Proxy-Authorization", authToken);

var client = new System.Net.Http.HttpClient();
var response = await client.SendAsync(request);
return await response.Content.ReadAsStringAsync();
```

If your outbound request uses the **HTTPS** protocol or a specific **port** you need to follow some extra steps:
1. Set the request URL to use `http` schema
2. Remove the port number from the URL (if any)
3. If you need to explicitly define the port number, add the `X-Vtex-Proxy-To` header with the actual URL you want. You don't need to include the _path_ here, just _schema_, _domain_ and _port_.
4. In case you don't need the port number, just add the `X-Vtex-Use-Https` header with value `true`.

Consider you want to send a request to the following URL:

`https://my-service.com:8090/_v/foo`

If you follow the steps above you will have something like this:

```C#
string authToken = HttpContext.Request.Headers["X-Vtex-Credential"];

var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("http://my-service.com/_v/foo")
};

request.Headers.Add("Proxy-Authorization", authToken);
request.Headers.Add("X-Vtex-Proxy-To", "https://my-service.com:8090");

var client = new System.Net.Http.HttpClient();
var response = await client.SendAsync(request);
return await response.Content.ReadAsStringAsync();
```

Without the port number, the request to `https://my-service.com/_v/foo` would be the same with a different header.

```C#
request.Headers.Add("Proxy-Authorization", authToken);
request.Headers.Add("X-Vtex-Use-Https", "true");
```

## GraphQL

This repository includes some GraphQL samples that you can use as base for your own project.

There are 2 approaches for writting GraphQL applications.

### Schema first
1. Define the `schema.graphql` file in the `graphql` folder.
2. Create your *query* class and decorate it with `[GraphQLMetadata("Query")]`.
3. Implement your resolvers following the [Schema First](https://graphql-dotnet.github.io/docs/getting-started/arguments#schema-first) syntax and decorate them with the same names you gave to your fields in the schema. Ex: `[GraphQLMetadata("books")]` for the `books` resolver.
4. Create your *mutation* class and decorate it with `[GraphQLMetadata("Mutation")]`.
5. Implement your resolvers the same way you did in step 3.

Example:
```C#
[GraphQLMetadata("Query")]
public class Query
{
    private readonly IBooksDataSource booksDataSource;
    public Query(IBooksDataSource booksDataSource)
    {
        this.booksDataSource = booksDataSource;
    }

    [GraphQLMetadata("books")]
    public IEnumerable<Book> GetBooks()
    {
        return booksDataSource.GetBooks();
    }
}
```

### Code first
1. Define the `schema.graphql` file in the `graphql` folder (this step should be automatic in the future).
2. Create your *query* class and decorate it with `[GraphQLMetadata("Query")]`.
3. Implement your resolvers following the [Code First](https://graphql-dotnet.github.io/docs/getting-started/arguments#graphtype-first) syntax. Make sure you use the same names you gave to your fields in the schema.
4. Create your *mutation* class and decorate it with `[GraphQLMetadata("Mutation")]`.
5. Implement your resolvers the same way you did in step 3.

Example:
```C#
[GraphQLMetadata("Query")]
public class Query : ObjectGraphType<object>
{
    public Query(IBooksDataSource booksDataSource)
    {
        Name = "Query";

        Field<ListGraphType<BookType>>(
            "books",
            resolve: context => booksDataSource.GetBooks()
        );
    }
}
```

**Tip:** You can only have one class for operation type (Query, Mutation or Subscription). If you want to separate your code by entity or responsibility you can do it with `partial` classes.

**Tip:** If you need to access the `HttpContext` in your resolvers you can inject the `IHttpContextAccessor` in your class constructor.

## Migrating from .Net 2.0 to .Net 2.2
In order to migrate your app from .Net 2.0 to .Net 2.2 you need to do the following:
1. Open your `manifest.json` file and change your "dotnet" builder from "0.x" to "1.x".
2. Open your project file (`*.csproj`) and change the `TargetFramework` from "netcoreapp2.0" to "netcoreapp2.2"
3. In your project file you also need to remove the reference to `Microsoft.AspNetCore.All` and add one for `Microsoft.AspNetCore.App`.

Your `manifest.json` file should look like this:

```
"builders": {
  "dotnet": "1.x",
  "graphql": "1.x"
},
```

And your project file should look like this:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.2</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="GraphQL" Version="2.4.0" />
  </ItemGroup>

</Project>
```