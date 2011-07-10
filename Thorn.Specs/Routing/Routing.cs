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

			var routingInfo = new RoutingInfo(exports, "fodder");

			_router = new CommandRouter(routingInfo);
		}

		[Test]
		public void ItShouldRouteASimpleCommand()
		{
			var command = new Command("bantha", null, "bantha", new string[0]);
			var expected = typeof (Fodder).GetMethod("Bantha");

			var export = _router.FindExport(command);
			export.Method.Should().Be.EqualTo(expected);
		}

		[Test]
		public void ItShouldRouteAScopedCommand()
		{
			var command = new Command("fodder:bantha", "fodder", "bantha", new string[0]);
			var expected = typeof(Fodder).GetMethod("Bantha");

			var export = _router.FindExport(command);
			export.Method.Should().Be.EqualTo(expected);
		}

	}
}
