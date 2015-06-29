# WepApi Exception Pipeline
Adds a pipeline of `ExceptionFilterAttribute` so we can execute multiple actions when an exception occurs. 

Adds a custom ExceptionFilter that can translate exceptions thrown inside WebApi actions to the right http status code and reason phrase. This enables us to expose real types in the WebApi actions instead of `IHttpActionResult`.

Also adds a exception filter that contains basic plumbing for logging WebApi exceptions. 

All in all three new filters: `PipelineExceptionFilterAttribute` `ExceptionTranslatorFilterAttribute`
and `ExceptionLoggerFilterAttribute`

## Example

To use the pipeline, simply add it to the `HttpConfiguration` object:

```c#
configuration.Filters.Add(
    new PipelineExceptionFilterAttribute()
        .Add( /* add filters here */));
```

To use the error translator, add it and register a list of exceptions that you want translated into useful statuses:

```c#
configuration.Filters.Add(
    new PipelineExceptionFilterAttribute()
        .Add(new ExceptionTranslatorFilterAttribute()
            .Register<UserNotLoggedInException>((exception, request) => new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "This is forbidden" })
            .Register<NotImplementedException>((exception, request) => new HttpResponseMessage(HttpStatusCode.NotImplemented) { ReasonPhrase = "Future feature" })
        )
    );
```

To add logging to the pipeline you can use the build in `ExceptionLoggerFilterAttribute` to log to any target:

```c#
configuration.Filters.Add(
    new PipelineExceptionFilterAttribute()
        .Add(new ExceptionLoggerFilterAttribute()
            .LogTo((exception, request) => Debug.WriteLine("EXCEPTION: " + request.RequestUri + " resulted in exception " + exception))));
```

So a full pipeline with exception translation and logging could look like this: 

```c#
configuration.Filters.Add(
    new PipelineExceptionFilterAttribute()
        .Add(new ExceptionTranslatorFilterAttribute()
            .Register<UserNotLoggedInException>((exception, request) => new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "This is forbidden" })
            .Register<NotImplementedException>((exception, request) => new HttpResponseMessage(HttpStatusCode.NotImplemented) { ReasonPhrase = "Future feature" })
        )
        .Add(new ExceptionLoggerFilterAttribute()
            .LogTo( (exception, request) => Debug.WriteLine("EXCEPTION: " + request.RequestUri + " resulted in exception " + exception))));
```


## How to

Simply add the Nuget package:

`PM> Install-Package WepApiExceptionPipeline`

## Requirements

You'll need .NET Framework 4.5.2 and WebApi 5.2.3 or later to use the precompiled binaries.

## License

WepApiExceptionPipeline is under the MIT license.
