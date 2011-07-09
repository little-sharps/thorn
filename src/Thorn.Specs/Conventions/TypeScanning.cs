using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;
using Thorn.Config;
using Thorn.Conventions;
using Thorn.Specs.ScanningFodder;

namespace Thorn.Specs.Conventions
{
	[TestFixture]
	public class TypeScanning
	{
		private IEnumerable<Export> _exports;
		private Type _type;

		[SetUp]
		public void SetUp()
		{
			var convention = new DefaultTypeScanningConvention();
			var scanner = new TypeScanner(convention);
			var typeSource = new AssemblyScanTypeSource(Assembly.GetExecutingAssembly(), "Thorn.Specs.ScanningFodder");
			_exports = scanner.GetExportsIn(typeSource);
			_type = typeof(Fodder);
		}

		[Test]
		public void ItShouldLocateAllDecoratedTypes()
		{
			var typesFound = _exports.Select(e => e.Type).Distinct();

			typesFound.Should().Contain(_type);
			typesFound.Count().Should().Be.EqualTo(1);
		}

		[Test]
		public void ItShouldLocatePublicMembers()
		{
			_exports.Select(e => e.Method).Should().Contain(_type.GetMethod("Bantha"));
			_exports.Select(e => e.Method).Should().Contain(_type.GetMethod("Hey"));
		}

		[Test]
		public void ItShouldNotLocateInheritedMembers()
		{
			var expected = _type.GetMethod("ToString");

			_exports.Select(e => e.Method).Should().Not.Contain(expected);
		}


		[Test]
		public void ItShouldNotLocatePublicMembersDecoratedWithIgnoreAttribute()
		{
			var expected = _type.GetMethod("Ignored");

			_exports.Select(e => e.Method).Should().Not.Contain(expected);
		}

		[Test]
		public void ItShouldUseLowercasedTypeNameAsNamespace()
		{
			var expected = "fodder";

			_exports.Select(e => e.Namespace).Should().Contain(expected);
		}

		[Test]
		public void ItShouldUseLowercasedMethodNameAsName()
		{
			var expected = "bantha";

			_exports.Select(e => e.Name).Should().Contain(expected);
		}

		[Test]
		public void ItShouldUseDescriptionAttributeIfAvailable()
		{
			_exports.First(e => e.Name == "bantha").Description.Should().Be.EqualTo("Bantha Description");
			_exports.First(e => e.Name == "hey").Description.Should().Be.Null();
		}
	}
}
