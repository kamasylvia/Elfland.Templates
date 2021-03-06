# Introduction
A template for YARP reverse proxy.

# Prerequisite
1. [.NET SDK](https://dotnet.microsoft.com/en-us/download)
2. [Docker](https://www.docker.com/get-started/)
3. One of the following system logger.
   1. [Expressionless](https://exceptionless.com/docs/self-hosting/docker/)
   2. [Seq](https://docs.datalust.co/docs/getting-started-with-docker)

# Usage
## Installation
Install the template from nuget.org.
```sh
dotnet new --install Elfland.Templates
```
## New project
```sh
dotnet new elfyarp -o <path to .csproj file>
```
## Running
Use `dotnet run` called within the sample's directory or `dotnet run --project <path to .csproj file>`

# Configuration
The configuration for YARP is defined in the `appsettings.json` file.
- [Configuration Files](https://microsoft.github.io/reverse-proxy/articles/config-files.html)

```json
{
  // Base URLs the server listens on, must be configured independently of the routes below
  "Urls": "http://localhost:5000;https://localhost:5001",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      // Uncomment to hide diagnostic messages from runtime and proxy
      // "Microsoft": "Warning",
      // "Yarp" : "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ReverseProxy": {
    // Routes tell the proxy which requests to forward
    "Routes": {
      "minimumroute" : {
        // Matches anything and routes it to www.example.com
        "ClusterId": "minimumcluster",
        "Match": {
          "Path": "{**catch-all}"
        }
      },
      "allrouteprops" : {
        // matches /something/* and routes to "allclusterprops"
        "ClusterId": "allclusterprops", // Name of one of the clusters
        "Order" : 100, // Lower numbers have higher precedence
        "Authorization Policy" : "Anonymous", // Name of the policy or "Default", "Anonymous"
        "CorsPolicy" : "Default", // Name of the CorsPolicy to apply to this route or "Default", "Disable"
        "Match": {
          "Path": "/something/{**remainder}", // The path to match using ASP.NET syntax.
          "Hosts" : [ "www.aaaaa.com", "www.bbbbb.com"], // The host names to match, unspecified is any
          "Methods" : [ "GET", "PUT" ], // The HTTP methods that match, uspecified is all
          "Headers": [ // The headers to match, unspecified is any
            {
              "Name": "MyCustomHeader", // Name of the header
              "Values": [ "value1", "value2", "another value" ], // Matches are against any of these values
              "Mode": "ExactHeader", // or "HeaderPrefix", "Exists" , "Contains", "NotContains"
              "IsCaseSensitive": true
            }
          ],
          "QueryParameters": [ // The query parameters to match, unspecified is any
            {
              "Name": "MyQueryParameter", // Name of the query parameter
              "Values": [ "value1", "value2", "another value" ], // Matches are against any of these values
              "Mode": "Exact", // or "Prefix", "Exists" , "Contains", "NotContains"
              "IsCaseSensitive": true
            }
          ]
        },
        "MetaData" : { // List of key value pairs that can be used by custom extensions
          "MyName" : "MyValue"
        },
        "Transforms" : [ // List of transforms. See the Transforms article for more details
          {
            "RequestHeader": "MyHeader",
            "Set": "MyValue",
          }
        ]
      }
    },
    // Clusters tell the proxy where and how to forward requests
    "Clusters": {
      "minimumcluster": {
        "Destinations": {
          "example.com": {
            "Address": "http://www.example.com/"
          }
        }
      },
      "allclusterprops": {
        "Destinations": {
          "first_destination": {
            "Address": "https://contoso.com"
          },
          "another_destination": {
            "Address": "https://10.20.30.40",
            "Health" : "https://10.20.30.40:12345/test" // override for active health checks
          }
        },
        "LoadBalancingPolicy" : "PowerOfTwoChoices", // Alternatively "FirstAlphabetical", "Random", "RoundRobin", "LeastRequests"
        "SessionAffinity": {
          "Enabled": true, // Defaults to 'false'
          "Policy": "Cookie", // Default, alternatively "CustomHeader"
          "FailurePolicy": "Redistribute", // default, Alternatively "Return503Error"
          "Settings" : {
              "CustomHeaderName": "MySessionHeaderName" // Defaults to 'X-Yarp-Proxy-Affinity`
          }
        },
        "HealthCheck": {
          "Active": { // Makes API calls to validate the health.
            "Enabled": "true",
            "Interval": "00:00:10",
            "Timeout": "00:00:10",
            "Policy": "ConsecutiveFailures",
            "Path": "/api/health" // API endpoint to query for health state
          },
          "Passive": { // Disables destinations based on HTTP response codes
            "Enabled": true, // Defaults to false
            "Policy" : "TransportFailureRateHealthPolicy", // Required
            "ReactivationPeriod" : "00:00:10" // 10s
          }
        },
        "HttpClient" : { // Configuration of HttpClient instance used to contact destinations
          "SSLProtocols" : "Tls13",
          "DangerousAcceptAnyServerCertificate" : false,
          "MaxConnectionsPerServer" : 1024,
          "EnableMultipleHttp2Connections" : true,
          "RequestHeaderEncoding" : "Latin1" // How to interpret non ASCII characters in header values
        },
        "HttpRequest" : { // Options for sending request to destination
          "ActivityTimeout" : "00:02:00",
          "Version" : "2",
          "VersionPolicy" : "RequestVersionOrLower",
          "AllowResponseBuffering" : "false"
        },
        "MetaData" : { // Custom Key value pairs
          "TransportFailureRateHealthPolicy.RateLimit": "0.5", // Used by Passive health policy
          "MyKey" : "MyValue"
        }
      }
    }
  }
}
```