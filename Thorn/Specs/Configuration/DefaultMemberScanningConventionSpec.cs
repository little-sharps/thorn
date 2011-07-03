using System.Linq;
using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;
using Thorn.Conventions;

namespace Thorn.Tests.Configuration
{
	[TestFixture]
	public class DefaultMemberScanningConventionSpec
	{
		[Test]
		public void ItShouldLocatePublicMembers()
		{
			var scanner = new DefaultMemberScanningConvention();
			var type = typeof(ScanningFodder.Fodder);

			var actions = scanner.MethodsToExport(type);

			var expected = type.GetMethod("Bantha");

			actions.Select(a => a.Method).Should().Contain(expected);
		}

		[Test]
		public void ItShouldNotLocateInheritedMembers()
		{
			var scanner = new DefaultMemberScanningConvention();
			var type = typeof(ScanningFodder.Fodder);

			var actions = scanner.MethodsToExport(type);

			var expected = type.GetMethod("ToString");

			actions.Select(a => a.Method).Should().Not.Contain(expected);
		}


		[Test]
		public void ItShouldNotLocatePublicMembersDecoratedWithIgnoreAttribute()
		{
			var scanner = new DefaultMemberScanningConvention();
			var type = typeof(ScanningFodder.Fodder);

			var actions = scanner.MethodsToExport(type);
			var expected = type.GetMethod("Ignored");

			actions.Select(a => a.Method).Should().Not.Contain(expected);
		}

	}
}