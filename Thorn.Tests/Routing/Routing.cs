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
        [Test]
        public void ItShouldRouteASimpleCommandName()
        {
            var router = Thorn.Configuration.Configure(Assembly.GetExecutingAssembly(), "Thorn.Tests.ScanningFodder");
            var commandName = "bantha";
            var expected = typeof (ScanningFodder.Fodder).GetMethod("Bantha");

            var action = router.FindAction(commandName);
            action.Method.Should().Be.EqualTo(expected);
        }

        [Test]
        public void ItShouldRouteAScopedCommandName()
        {
            var router = Thorn.Configuration.Configure(Assembly.GetExecutingAssembly(), "Thorn.Tests.ScanningFodder");
            var commandName = "fodder:bantha";
            var expected = typeof(ScanningFodder.Fodder).GetMethod("Bantha");

            var action = router.FindAction(commandName);
            action.Method.Should().Be.EqualTo(expected);
        }

    }
}
