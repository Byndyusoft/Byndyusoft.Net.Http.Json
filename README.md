## Byndyusoft.Net.Http.Json
[![(License)](https://img.shields.io/github/license/Byndyusoft/Byndyusoft.Net.Http.Json.svg)](LICENSE.txt)
[![Nuget](http://img.shields.io/nuget/v/Byndyusoft.Net.Http.Json.svg?maxAge=10800)](https://www.nuget.org/packages/Byndyusoft.Net.Http.Json/) [![NuGet downloads](https://img.shields.io/nuget/dt/Byndyusoft.Net.Http.Json.svg)](https://www.nuget.org/packages/Byndyusoft.Net.Http.Json/) 

This package adds `JsonMediaTypeFormatter` class for formatting `HttpClient` requests and responses:

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

### Installing

```shell
dotnet add package Byndyusoft.Net.Http.Json
```

***

# Contributing

To contribute, you will need to setup your local environment, see [prerequisites](#prerequisites). For the contribution and workflow guide, see [package development lifecycle](#package-development-lifecycle).

A detailed overview on how to contribute can be found in the [contributing guide](CONTRIBUTING.md).

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Git - [Download & Install Git](https://git-scm.com/downloads). OSX and Linux machines typically have this already installed.
- .NET (version 8.0 or higher) - [Download & Install .NET Core](https://dotnet.microsoft.com/download/dotnet/8.0).

## General folders layout

### src
- source code

### tests

- unit-tests

## Package development lifecycle

- Implement package logic in `src`
- Add or addapt unit-tests (prefer before and simultaneously with coding) in `tests`
- Add or change the documentation as needed
- Open pull request in the correct branch. Target the project's `master` branch

# Maintainers

[github.maintain@byndyusoft.com](mailto:github.maintain@byndyusoft.com)