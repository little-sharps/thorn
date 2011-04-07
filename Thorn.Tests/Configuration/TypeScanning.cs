using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;

namespace Thorn.Tests.Configuration
{
    [TestFixture]
    public class TypeScanning
    {
        [Test]
        public void ItShouldLocateAllDecoratedTypes()
        {
            var scanner = new AssemblyScanner();
            var type = typeof(ScanningFodder.Fodder);

            Type[] typesFound = scanner.GetDecoratedTypesIn(Assembly.GetAssembly(type), "Thorn.Tests.ScanningFodder");

            typesFound.Should().Contain(type);
            typesFound.Count().Should().Be.EqualTo(1);

        }
    }
}
