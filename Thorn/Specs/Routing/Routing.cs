using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx;
using Thorn.Config;
using Thorn.Specs.ScanningFodder;

namespace Thorn.Specs.Routing
{
	[TestFixture]
	public class Routing
	{
		private CommandRouter _router;

		[SetUp]
		public void BeforeEach()
		{
			var type = typeof (Fodder);
			var exports = new List<Export>
			              {
			              	new Export(type, type.GetMethod("Bantha"), "fodder", "bantha")
			              };

			var routingInfo = new RoutingInfo(exports);

			_router = new CommandRouter(routingInfo);
		}

		[Test]
		public void ItShouldRouteASimpleCommandName()
		{
			var commandName = "bantha";
			var expected = typeof (Fodder).GetMethod("Bantha");

			var export = _router.FindExport(commandName);
			export.Method.Should().Be.EqualTo(expected);
		}

		[Test]
		public void ItShouldRouteAScopedCommandName()
		{
			var commandName = "fodder:bantha";
			var expected = typeof(Fodder).GetMethod("Bantha");

			var export = _router.FindExport(commandName);
			export.Method.Should().Be.EqualTo(expected);
		}

	}
}
