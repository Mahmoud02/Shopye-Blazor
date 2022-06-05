# Overview
This Demo Project presents how to develop a Blazor UI application and show how to organize different components to build the UI and the communictaion with API.
## What is Blazor
Blazor lets you build interactive web UIs using C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries. 

## Single-Page Applications
In web development, you want to show something in a browser, and nowadays you would use a framework like Angular or Vue or React to create your UI.   
These frameworks are great at developing fast and interactive UIs, and they all run on JavaScript, the scripting language of the web.  
This is fine, but it does mean that there is a fundamental disconnect.**You can't use the language that you use for your server-side code to create UIs** for the browser,so you can't reuse your skillset, and some of these UI frameworks have a pretty steep learning curve.  

Wouldn't it be great if you could use the same language on your server, in your libraries, and in the UI,in fact, you can run any type of code in a browser using **WebAssembly**. 

### WebAssembly
WebAssembly is a way to run bytecode in a browser inside the JavaScript runtime sandbox. You can compile any type of code, like C++ or C#, to WebAssembly bytecode and run it in a browser at near-native speed without the need for a plugin, and that is because WebAssembly is a native part of all the major browsers, including mobile browsers.  

### Blazor
Blazor is the only **.NET-based Single-Page Application (SPA) framework in existence**, in Blazor, we can reuse existing libraries. Blazor is just a framework that runs on the .NET runtime, so we can use any library that we want to use, as long as it is compatible with .NET Standard. This means that we can use our own libraries and also almost all public NuGet packages.

## Blazor hosting models
There are two primary Blazor hosting models, either Blazor Server or Blazor WebAssembly. 
### Blazor Server
With Blazor Server, when a client browser makes the initial request to the web server the server executes .NET code to generate an HTML response dynamically,  HTML is returned, and subsequent requests are made to fetch CSS and JavaScript as specified in the HTML document.  

Once the application is loaded and running, clientside routing and **other UI updates are made possible with an ASP.NET Core SignalR connection**.  

ASP.NET Core SignalR offers **a bidirectional communication between client and server**, sending messages in real-time. This technology is used to communicate changes to the document object model (DOM) on the client browser — **without a page refresh**.  

In this architecture:
1. the Blazor app runs inside an ASP.NET website that runs on the .NET runtime. 
2. This website then serves the Blazor website and loads Blazor through a WebSocket connection that it uses through SignalR. 
3. UI updates are streamed from the server in real time. This sounds complicated, but you don't have to do anything special to make this work. This all comes out of the box. 
4. code in a Blazor Server application can also interact with JavaScript, just like code in Blazor WebAssembly can.
5. You can use Blazor Server for thin client scenarios. These are scenarios where you want to run your app on a client with limited resources and where it might not have WebAssembly. 
6. It uses a real-time WebSocket connection through the SignalR framework to update UI elements. 
7. because you run your app on a server instead of the client, you have access to everything that an ASP.NET server has to offer. 
8. This includes the ability to easily access databases and APIs from the server and the ability to store secrets safely.
9. Every client needs to have a connection to the server,luckily, you can scale these connections by using an Azure SignalR service, which is scalable by design. 
#### Let's look at the pros and cons of Blazor Server.
1. We'll start with the pros. 
 - Everything runs on the server, so there is no need to download the .NET runtime or all the assemblies of the app. This means that the app loads fast. 
 - The component code isn’t served to clients, only the resulting HTML. 
 - because you run on the server, you have access to the full capabilities of an ASP.NET server. 
 - WebAssembly is not required to run the app, which can be handy in thin client scenarios. 
 - you can store secrets safely on the server without uploading them to every client. 
 - Server capabilities are present with the Blazor Server hosting model, as the app technically runs on the server.
2. here are the disadvantages of Blazor Server. 
 - You need a connection to the server, so you can't work offline. 
 - you need a server to run this, so you need to maintain and secure that server and keep it up and running. 
 - because you constantly go back to the server for UI updates, you'll have a higher latency than with the client app, which means that your application is slower than a Blazor WebAssembly application. And slower doesn't mean slow.your app will still be very fast, just not as fast as when it runs on the client in WebAssembly.


### Blazor WebAssembly hosting model
With Blazor WebAssembly, when a client browser makes the initial request to the web server the server returns static HTML. Subsequent requests are made to fetch CSS and JavaScript as specified in the HTML document.  

As part of a Blazor WebAssembly app’s HTML, there will be a <link>element that request the **blazor.webassembly.js** file,This file executes and starts loading WebAssembly. This acts as a bootstrap, which requests .NET binaries from the server.  

1. When the Blazor app loads, it downloads everything that it needs to the browser. 
2. This is everything that makes up the web page, including HTML, CSS, maybe some JavaScript, and possibly images. 
3. It also downloads the assemblies that make up the application, and it even downloads the complete .NET runtime that is converted into WebAssembly bytecode. 
4. All of this runs completely on WebAssembly, which is a part of all major web browsers, including mobile browsers. 
5. This type of Blazor application doesn't need a connection to a server. It just needs to load into the browser, and that's it.
6. Running your app on WebAssembly is fast. You get near-native performance, which is really fast.
7. Your app can work completely offline because you don't need a connection to a server.
8. your app can work completely offline because you don't need a connection to a server.
9. This architecture uses the processing resources of the client device, so the browser and the device it runs on does all the work. 
10. WebAssembly is a native part of all major browsers. No plugin needed.
11. This keeps things simple and makes it easy to deploy to all sorts of devices without worrying about the prerequisites.

#### There are also downsides to running everything in the browser. 
1. The first thing is that you are restricted to the capabilities of the browser. 
2. Your code runs in a sandbox, and you have to behave within the rules of that sandbox. 
3. And the browser does all the work. We just said that this can be a benefit, but it can also be a downside. 
4. If you need to do compute-intensive things, you might not have enough processing power to do so. 
5. Blazor WebAssembly downloads everything to the browser, including the .NET runtime. This takes time, so loading the app will be relatively slower than when you don't have to do this. 
6. But the good thing is that it caches all of these things, including the .NET runtime, so the wasm file. So when the user visits your app again, it loads these resources from the browser cache and loads the app extremely fast, so it's not that bad after all.
7. when you need to store a key to connect to an API, you can only store it in a file that is downloaded to the client. There are ways to encrypt secrets like that, but it is still not very safe. 
8. The final disadvantage is that WebAssembly is required. Sure, it is all part of major browsers, but sometimes you need your app to run on older browsers that might not have WebAssembly.

### Final Thought
 When you need near-native performance, for instance, when you are creating a game, you should use Blazor WebAssembly. And when you need to connect to things like databases and APIs, you'd be better off using Blazor Server. You can do this in Blazor WebAssembly, but you would be storing your credentials on every client. And when you cannot be sure that WebAssembly is on your user's device, you should use Blazor Server. Okay, so when you want to run your application offline, you use Blazor WebAssembly because it doesn't need to connect to the server. And when you don't want to run a fully featured server and be responsible for maintaining it and keeping it up and running, you also use Blazor WebAssembly. And finally, when you want to create performant, rich, and interactive applications for the web with C#, you can use Blazor in any form
## Code reuse
SPA developers have been fighting a losing battle for years, where web API endpoints define a payload in a certain shape — and the developer has to understand the shape of each endpoint. Consuming client-side code has to model the same shape, this is error-prone as the server can change the shape of an API whenever it needs to. The client would have to adapt, and this is tedious. Blazor can alleviate that concern by sharing models from .NET Web APIs, with the Blazor client app. I cannot stress the importance of this enough. Sharing the models from a class library with both the server and the client is like having your cake and eating it too.

## Blazor Design Patterns
As you start to use Blazer, you might notice some design similarities to some of the popular JavaScript frameworks out there. Blazer is a component driven framework. Components allow us to break apart. Web-page is into reusable, isolated pieces that can be easily moved around the site. These components can do each communicate between one another and the server and have their own life cycles. 

Single page apps dynamically rewrite the same web-page in response to user actions. They also rely on background requests through AJAX or Web sockets to retrieve updated data or mark up. Rather than reloading that entire page from the server en blazer, each component is able to independently communicate with the server or other parts of the page. Even as users switch between conceptual pages on the navigation bars, Blazer is simply calculating changes to the page and rewriting the markup. 

Single page apps provide a fluid and responsive experience for the user and avoid those constant full page reloads, which can also improve performance. 

## Dependency Injection Fundamentals
dependency injection or D. I is a critical aspect of a properly designed blazer application .
Conceptually dependency injection is a design pattern that implements inversion of control and dependency inversion principles. In generalized terms, these ideas state that high level classes or components should not depend on low level component implementations or be responsible for creating them. Instead, they should depend on abstractions that are provided to them in order to create loosely coupled components. .

A dependency injection container is responsible for supplying instances of dependencies throughout the application. So let's say we have two different blazer components and they each declared dependencies on different services using interfaces. The container could be configured to supply specific implementations of those contracts when our components error created by the framework, The D I container is then responsible for supplying instances of those interfaces. .NET Core provides easy access to a default container through the services collection, which could be configured in the startup class. 

Dependency injection can also help manage the lifecycle of different instances throughout our app. 

one of the most important dependency injection concepts in .NET is three idea of service lifetimes. So let's review what these lifetimes are and how they apply to the Blazer framework. In simple terms, a service lifetime determines when a new instance of a dependency is created by the D. I. Container, for example, is a new service created for every request or only once during the life of the application. .NET core provides useful features that allow us to easily manage and understand this idea in both Blazer Server and web assembly.

Let's start by exploring these lifetimes in Blazer Server. In the previous clips, we saw how to register classes with the D I container using the ad Transient Helper. When dependencies are registered with this method, a new instances created every request for every class or component that needs it. Another option is AD DS Singleton, which creates one instance of a service for the entire time the application is running. That instance is shared across all the different requests and components. The final option is AD DS scoped, which will cause a service to be reused for all requests in the same circuit. So then you might be wondering, Well, what is a circuit? Well, a blazer circuit is an abstraction over the SignalR connection between the browser and the server that helps to manage our state and scope. Blazer circuits are crucial for rendering UI updates seamlessly on the server. This means circuits only exist in Blazer server hosting models and not in the web assembly model, since those apps run entirely in the browser.

So let's say we have a user who visits are Blazer Server app in the browser, which creates a SignalR connection to our server to load the page. This connection is treated as a new blazer circuit. Closing the browser tab or navigating to another site would terminate the circuit and release those resource is now. Let's say that page they visit has two components, which have a dependency on a message service that is registered with the AD DS sculpt method. This means the D I container will create one instance of that dependency and that will be reused among the components of that circuit. If a second users were to visit that page on the site, a second circuit would be created. A second instance of the dependency would also be created and shared among the components of that circuit. Now let's look at service lifetimes in the context of web assembly. Both transient and singleton scopes work exactly the same way and web assembly with transients a new instance will be created every time it is needed, and Singleton's will result in one instance that shared across the app, however, AD DS scoped behaves differently in web assembly, and this makes sense if we think about it. Blazer circuits do not exist in web assembly, since there is no SignalR round trip to the server to manage state. This means that in web assembly, services registered with AD DS scoped are also simply treated as Singleton's. 

## Understanding the Blazor Routing System
Conceptually routing is very similar in Blazer Server and web assembly, so the actual code and usage for both is almost identical. However, there are some differences in the underlying implementation details. 

So let's start with Blazers server and let's say that a request comes in from the home page of our app. By default, that request is routed to the host AD CS HTML Razor page, which is rendered back to the browser and acts as the main container for our blazer app. This host file also includes a SignalR JavaScript library and a marker comment in the HTML. Together, these items bootstrap are blazer app, and they create a new WebSocket circuit connection with the server. As we navigate the site, those you are. All changes are intercepted and sent over the blazer circuit as messages and events rather than his. HTTP requests for entirely new pages or new HTML documents, a component called the Blazer router response to these events by locating a component with a matching route template for the requested URL. The selected component is then rendered and sent back to the browser as an HTML fragment, where it is used to rewrite part of the original page that has changed. Remember, with single page apps, we are continuously rewriting the DOM of the same page only where updates error necessary. The Blazer circuit also stays open to manage future interactions by the user as well. Where is that initial HTTP requests to load the container page only happens once to make all of this work. Blazer Server also requires a few routing configurations in startup. Do CSS map Blazer Hub simply sets up the connection path. For that SignalR circuit map, Fall back to page is a little more interesting. This method routes all incoming requests to the host razor page that contains our blazer app. 

The overall routing process and web assembly is similar to Blazers server, but without the overhead of a SignalR circuit and trips to the server. So when we visit the home page of our site, these index dot html pages downloaded along with other static assets like CSS files. The index page also references a Blazer web assembly, JavaScript Library. This library will download all of the necessary DLL els for our app and locate the app component on our index page to bootstrap are blazer application. From that point on, navigation clicks are intercepted by blazer and still handled by that familiar router component. The router locates a matching component from the downloaded assemblies and renders in place updates on the same page, just like we saw with Blazers server. The main difference is just that all of this happens right in the browser rather than over a circuit connection to the server Blazer web assembly also requires fewer application configurations to work properly. For example, the map, blazer hub and fall back page configurations we looked at earlier are not present in web assembly projects. A SignalR hub is not needed since there are no circuit round trips to the server. We also do not need to map fall back pages, since the MVC and Razor pages framework options are not available to run in the browser alongside Blazer in the first place. 
## Exploring Application State
## HTTP Communication with Blazor
A RESTful web service uses a combination of structured Urals and HTTP verbs to map requests to certain endpoints. So, as an example are get jobs. Method shares the same route template as our create jobs method, but her application will map them differently whether the incoming request is a get or post type. Now, when working with external APIs like this, it's unlikely you'll have full access to the source code to figure all this out. Instead, you're usually provided documentation about the various endpoints. In some cases, you might be lucky enough to have access to something like swagger. UI Swagger UI provides visual and interactive documentation based on the swagger specifications, and it allows users to explore an APIs endpoints without the source code.
### Calling a Web API with Blazor
Let's explore the features Blazer provides for working with HTTP requests. The overall process is really not that different from other .NET options, but there are a few unique considerations. Blazer uses a class called HTTP client to make requests to APIs similar to the rest of .NET core. This is true of both Blazer web Assembly and Blazers server. However, there are some differences between the two hosting models that error worth discussing. So for Blazer web assembly, the HTTP client is mostly ready to go out of the box. We can simply inject it directly into our components or services, at least for simpler use cases. We do not have to worry too much about where that instance comes from and how it's managed for blazers server. We can also inject HTTP client in a similar way, but some additional configurations using a factory class are required to do so correctly.

