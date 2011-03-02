using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Thorn.Tests.SampleHandlers;

namespace Thorn.Tests.Configuration
{
    [TestFixture]
    public class AttributeBasedConfiguration
    {
        [Test]
        public void ItShouldFindInterestingHandlers()
        {
            Thorn.ConfigureHandlers()
                .WithAttribute(typeof(SampleAttribute))
                .InAssembly(Assembly.GetAssembly(typeof(AttributeDecoratedHandler)));

            Thorn.Configuration.KnownHandlers.Should().Contain(typeof(AttributeDecoratedHandler));
        }
        
    }
}
