using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            var scanner = new AssemblyScanner();
            var actions = scanner.GetActionsFrom(Assembly.GetExecutingAssembly(), "Thorn.Tests.ScanningFodder");
            _router = new CommandRouter(actions, typeof(ScanningFodder.Fodder));
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
