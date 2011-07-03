using NUnit.Framework;
using SharpTestsEx;

namespace Thorn.Tests.Routing
{
	[TestFixture]
	public class Routing
	{
		private CommandRouter _router;

		[SetUp]
		public void BeforeEach()
		{
			var type = typeof (ScanningFodder.Fodder);
			var scanner = new DefaultMemberScanningConvention();
			var config = new Thorn.Configuration();
			config.AddExport(new Export(type, false, "fodder", scanner.GetActions(type)));

			_router = new CommandRouter(config);
		}

		[Test]
		public void ItShouldRouteASimpleCommandName()
		{
			var commandName = "bantha";
			var expected = typeof (ScanningFodder.Fodder).GetMethod("Bantha");

			var action = _router.FindAction(commandName);
			action.Method.Should().Be.EqualTo(expected);
		}

		[Test]
		public void ItShouldRouteAScopedCommandName()
		{
			var commandName = "fodder:bantha";
			var expected = typeof(ScanningFodder.Fodder).GetMethod("Bantha");

			var action = _router.FindAction(commandName);
			action.Method.Should().Be.EqualTo(expected);
		}

	}
}
