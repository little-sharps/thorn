using System.Reflection;
using NUnit.Framework;
using SharpTestsEx;

namespace Thorn.Tests.Configuration
{
    [TestFixture]
    public class MemberScanning
    {
        [Test]
        public void ItShouldLocatePublicMembers()
        {
            var scanner = new AssemblyScanner();
            var type = typeof(ScanningFodder.Fodder);

            MethodInfo[] methodsFound = scanner.GetRoutableMethodsOn(type);

            var expected = type.GetMethod("Bantha");

            methodsFound.Should().Contain(expected);
        }

        [Test]
        public void ItShouldNotLocateInheritedMembers()
        {
            var scanner = new AssemblyScanner();
            var type = typeof(ScanningFodder.Fodder);

            MethodInfo[] methodsFound = scanner.GetRoutableMethodsOn(type);

            var expected = type.GetMethod("ToString");

            methodsFound.Should().Not.Contain(expected);
        }


        [Test]
        public void ItShouldNotLocatePublicMembersDecoratedWithIgnoreAttribute()
        {
            var scanner = new AssemblyScanner();
            var type = typeof(ScanningFodder.Fodder);

            MethodInfo[] methodsFound = scanner.GetRoutableMethodsOn(type);

            var expected = type.GetMethod("Ignored");

            methodsFound.Should().Not.Contain(expected);
        }

    }
}