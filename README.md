Topshelf.winformsHost
=====================

Topshelf.WinformsHost provides extensions to run your services in a winforms environment (when started interavtively), instead of the default console environment.

Install
-------
It's available via [nuget package](https://www.nuget.org/packages/topshelf.winformshost)  
PM> `Install-Package Topshelf.WinformsHost`

Example Usage
-------------
```csharp
static void Main(string[] args)
{
        HostFactory.Run(c =>
        {
        	c.UseWinformsHost();

            c.Service<SampleService>());
        });
}
```
