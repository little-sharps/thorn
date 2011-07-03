using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;
using Thorn.Conventions;

namespace Thorn.Tests.Configuration
{
    [TestFixture]
    public class DefaultTypeScanningConfigurationSpec
    {
        [Test]
        public void ItShouldLocateAllDecoratedTypes()
        {
            var scanner = new DefaultTypeScanningConvention();
            var type = typeof(ScanningFodder.Fodder);

        	var types = new List<Type>() { type }.AsQueryable();
            var exports = scanner.GetExports(types, new DefaultMemberScanningConvention());

        	var typesFound = exports.Select(e => e.Type);

            typesFound.Should().Contain(type);
            typesFound.Count().Should().Be.EqualTo(1);

        }
    }
}
