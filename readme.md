Thorn (Thor for .NET)
=====================
A command line utility accelerator.

In 30 secs
----------
For building a "Tasks" style command line utility. To wit:
 1. In VS2010, Create a new "Console Application" project.
 2. Reference Thorn. (NuGet coming soon)
 3. Make your code look like this:

Program.cs:

	class Program
	{
		static void Main(string[] args)
		{
			Thorn.Runner.Run(args);
		}
	}

Exports.cs:

	[ThornExport]
	public class Exports
	{
		[Description("Says 'Hello'")]
		public void Hello()
		{
			Console.WriteLine("Hello");
		}
	}

 4. Run it in the command window:
	C:\...\bin\Debug>MyApp.exe hello

 5. Get Usage Info:
	C:\...\bin\Debug>MyApp.exe help
	C:\...\bin\Debug>MyApp.exe help hello