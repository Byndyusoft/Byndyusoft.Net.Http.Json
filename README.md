## Byndyusoft.Net.Http.Json
This package adds `JsonMediaTypeFormatter` class for formatting `HttpClient` requests and responses.

### Installing

```shell
dotnet add package Byndyusoft.Net.Http.Json
```

## Usage

```csharp
using (var client = new HttpClient())
{
	var formatter = new JsonMediaTypeFormatter();
	var request = new SearchProductRequest { Name = 'iphone', OrderBy = 'id' };
	var content = new ObjectContent<SearchProductRequest>(request, formatter);
	var response = await client.PostAsync("http://localhost/api/products:search", content);
	var products = await response.Content.ReadAsAsync<Product[]>(new[] {formatter});
}
```

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For a contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET Core (version 6.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## Package development lifecycle

- Implement package logic in `src`
- Add or adapt unit-tests in `tests`
- Add or change the documentation as needed
- Open pull request for a correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)