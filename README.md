# Blazor
This Demo Project presents how to develop a Blazor UI application and show how to organize different components to build the UI and the communictaion with API.
## What is Blazor
Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries. 
## Blazor hosting models
There are two primary Blazor hosting models, either Blazor Server or Blazor WebAssembly. 
### Blazor Server
With Blazor Server, when a client browser makes the initial request to the web server the server executes .NET code to generate an HTML response dynamically,  HTML is returned, and subsequent requests are made to fetch CSS and JavaScript as specified in the HTML document. 
Once the application is loaded and running, clientside routing and **other UI updates are made possible with an ASP.NET Core SignalR connection**. 
ASP.NET Core SignalR offers a bidirectional communication between client and server, sending messages in real-time. This technology is used to communicate changes to the document object model (DOM) on the client browser — **without a page refresh**. 

#### Advantages to using Blazor Server as a hosting model 
1. The download size is smaller than Blazor WebAssembly, as the app is rendered on the server. 
2. The component code isn’t served to clients, only the resulting HTML. 
3. Server capabilities are present with the Blazor Server hosting model, as the app technically runs on the server.

### Blazor WebAssembly hosting model
With Blazor WebAssembly, when a client browser makes the initial request to the web server the server returns static HTML. Subsequent requests are made to fetch CSS and JavaScript as specified in the HTML document.
As part of a Blazor WebAssembly app’s HTML, there will be a <link>element that request the **blazor.webassembly.js** file,This file executes and starts loading WebAssembly. This acts as a bootstrap, which requests .NET binaries from the server.

**With Blazor WebAssembly hosting, all of your C# code is executed on the client. This means that you should avoid using any code that requires server-side functionality, and you should avoid sensitive data such as passwords, API keys, or other sensitive information.**

With the Blazor WebAssembly hosting model, you can write C# that compiles to WebAssembly.
This means it’s possible to take C, C++, Rust, C#, and other non-traditional web programming languages and target WebAssembly for their compilation. This results in WebAssembly binaries, which are web-runnable based on open standards but from programming languages other than JavaScript.

## Single-Page Applications
Blazor is the only .NET-based Single-Page Application (SPA) framework in existence. 
There are many popular JavaScript SPA frameworks including (Angular,React,Vue)
## Code reuse
SPA developers have been fighting a losing battle for years, where web API endpoints define a payload in a certain shape — and the developer has to understand the shape of each endpoint. Consuming client-side code has to model the same shape, this is error-prone as the server can change the shape of an API whenever it needs to. The client would have to adapt, and this is tedious. Blazor can alleviate that concern by sharing models from .NET Web APIs, with the Blazor client app. I cannot stress the importance of this enough. Sharing the models from a class library with both the server and the client is like having your cake and eating it too.

