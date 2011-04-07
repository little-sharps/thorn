using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Thorn.Tests.Configuration
{
    [TestFixture]
    public class GettingExportedActions
    {
        [Test]
        public void ItShouldReturnAllExportedActions()
        {
            var scanner = new AssemblyScanner();
            
            var actions = scanner.GetActionsFrom(Assembly.GetExecutingAssembly(), "Thorn.Tests.ScanningFodder");

            var expected = typeof (ScanningFodder.Fodder).GetMethod("Bantha");

            actions.First().Method.Should().Be.EqualTo(expected);
        }
    }
}
