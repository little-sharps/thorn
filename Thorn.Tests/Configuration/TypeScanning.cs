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
            var scanner = new DefaultTypeScanningConvention();
            var type = typeof(ScanningFodder.Fodder);

        	var source = new AssemblyScanTypeSource(Assembly.GetAssembly(type), "Thorn.Tests.ScanningFodder");
            var exports = scanner.GetExports(source.Types, new DefaultMemberScanningConvention());

        	var typesFound = exports.Select(e => e.Type);

            typesFound.Should().Contain(type);
            typesFound.Count().Should().Be.EqualTo(1);

        }
    }
}
