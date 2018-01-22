# .Net Getting Started

A quick start app using the `dotnet` builder for building backend apps.

First, install the VTEX Toolbelt and login:

```bash
$ npm i -g vtex
$ vtex login
```

Download this repo and open a terminal in its folder.

Then, use `link` to start developing your app:

```bash
$ vtex link
```

Finally, access your public endpoint at:

https://{{workspace}}--{{account}}.myvtex.com/api/vtex/hello

You can also access private endpoints, but you need to send your token in the `Authorization` header.

VTEX Toolbelt will copy your token to the clipboard if you run the following command:

```bash
$ vtex local token
```

Now that you already have a token you can call your private endpoint:

http://dotnet-getting-started.vtex.aws-us-east-1.vtex.io/{{account}}/{{workspace}}/api/vtex/private

## Setting up my application

Your application details (name, version and description, for instance) must be set at your `manifest.json` file.

```json
{
  "name": "Calk",
  "vendor": "MyCompany",
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