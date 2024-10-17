# Novacta.Analytics.Runtime

The **Novacta.Analytics.Runtime** library
provides functionality for native asset deployments
in a development environment having
references to the **Novacta.Analytics** project.

## Installation

Add a reference to the **Novacta.Analytics.Runtime** project.

## Usage

The **Novacta.Analytics.Runtime** library
provides class **AnalyticsRuntime**.

This class is used to deploy native assets
by calling method

```csharp
    AnalitycsRuntime.Deploy();
```

By calling this method, the native assets required by
the **Novacta.Analytics** library are copied to
the output directory of a project.

The **Deploy** method must be called before using types
from the **Novacta.Analytics** project.

## Example

```csharp
    using Novacta.Analytics.Runtime;
    using Novacta.Analytics;
    using System;

    namespace Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                AnalyticsRuntime.Deploy();
                
                DoubleMatrix l = DoubleMatrix.Dense(2, 4, -1.0);
                DoubleMatrix r = DoubleMatrix.Dense(4, 2, 2.0);
                var o = l * r;
                Console.WriteLine(o);
                Console.WriteLine("DOne!");
                Console.ReadKey();
            }
        }
    }
```

## Additional information

The **Novacta.Analytics.Runtime** is not required in
production environments referencing
the **Novacta.Analytics** NuGet package.
