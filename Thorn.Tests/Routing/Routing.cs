using System.Collections.Generic;
using NUnit.Framework;
using Thorn.Config;

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
			var config = new Config.Configuration();
			var actions = new List<Export>();
			actions.Add(new Export(type, type.GetMethod("Bantha"), "bantha"));
			config.AddExport(new Scope("fodder", false, actions));

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
