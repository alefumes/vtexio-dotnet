# .Net Getting Started

A quick start app using the `dotnet` builder for building backend apps.

For more information, please check our [developer docs](https://vtex.io).

## Getting Started

First, install the VTEX Toolbelt and login:

```bash
$ npm i -g vtex
$ vtex login
```

Download this repo and open a terminal in its folder.

To start coding you need a workspace (it's like a `git` branch of your store). Let's create a workspace named `dev`:

```bash
$ vtex use dev
```

Then, use `link` to start developing your app:

```bash
$ vtex link
```

Finally, access your public endpoint at:

`https://{{workspace}}--{{account}}.myvtex.com/api/vtex/hello`

You can also access private endpoints, but you need to send your token in the `Authorization` header.

VTEX Toolbelt will copy your token to the clipboard if you run the following command:

```bash
$ vtex local token
```

Now that you already have a token you can call your private endpoint:

`http://dotnet-getting-started.vtex.aws-us-east-1.vtex.io/{{account}}/{{workspace}}/api/vtex/private`

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

### Named Parameters

You can define named path parameters for your routes by adding a `:` before its names. The following example shows a route with a `code` parameter.

```json
...
"routes": {
  "cars": {
    "path": "/api/cars/:code",
    "public": true
  }
}
...
```

### Catch-all Parameters

The router will compare the route patterns in the order they are defined, so you need to make sure you set the more specific ones first.

If you need to define catch-all routes to handle requests that can't match your specific routes, you can do it by using an `*` before the name of the parameter.

In the example below you have a catch-all route that handles all requests that start with `/api` (`apiDefault`) and a more generic one to handle any request that couldn't match any previous pattern (`default`).

```json
...
"routes": {
  ...
  "apiDefault": {
    "path": "/api/*url",
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
