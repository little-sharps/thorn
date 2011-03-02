using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;

namespace Thorn.Tests.Routing
{
    public static class Matchers
    {
        public static void ShouldParseTo(this string[] args, IDictionary<string, object> expectedValues)
        {
            var routeDictionary = Thorn.GetRouteDictionary(args) as IDictionary<string, object>;
            foreach (var key in expectedValues.Keys)
            {
                Console.WriteLine("Comparing on key: {0}", key);
                routeDictionary.Keys.Should().Contain(key);
                if(routeDictionary[key] is IEnumerable<string>)
                {
                    var routeDataList = (IEnumerable<string>) routeDictionary[key];
                    var expectedList = (IEnumerable<string>) expectedValues[key];
                    routeDataList.Count().Should().Be.EqualTo(expectedList.Count());

                    for(var i = 0; i < expectedList.Count(); i++)
                    {
                        routeDataList.ElementAt(i).Should().Be.EqualTo(expectedList.ElementAt(i));
                    }
                }
                else
                {
                    routeDictionary[key].Should().Be.EqualTo(expectedValues[key]);
                }
            }
        }
    }

    [TestFixture]
    public class ParsingRoutingData
    {
        [Test]
        public void ItShouldComeUpEmptyOnAnEmptyString()
        {
            var args = new string[] {};
            var routeDictionary = Thorn.GetRouteDictionary(args) as IDictionary<string, object>;

            routeDictionary.Keys.Should().Not.Contain("MethodName");
        }

        [Test]
        public void ItShouldParseACommandName()
        {
            string[] args = new[] {"reformat", "/f", "c:"};

            dynamic expected = new ExpandoObject();
            expected.MethodName = "reformat";

            args.ShouldParseTo(expected as IDictionary<string, object>);
           
        }

        [Test]
        public void ItShouldParseANamespacedCommandName()
        {
            string[] args = new[] { "project:sweep", "thorn" };

            dynamic expected = new ExpandoObject();
            expected.Namespace = "project";
            expected.MethodName = "sweep";

            args.ShouldParseTo(expected as IDictionary<string, object>);
        }

        [Test]
        public void ItShouldStripTheParsedTermsFromTheArguments()
        {
            string[] args = new[] { "helm:layincourse", "/destination", "Farpoint Station", "/warpFactor", "8" };

            dynamic expected = new ExpandoObject();
            expected.Args = new[] { "/destination", "Farpoint Station", "/warpFactor", "8" };

            args.ShouldParseTo(expected as IDictionary<string, object>);
        }
    }
}
