using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Thorn;

namespace ThornSample
{
    [ThornExport]
    public class Exports
    {
        [Description("Says 'Hello'")]
        public void Hello()
        {
            Console.WriteLine("Hello");
        }

        [Description("Calculates the volume of a cylinder given a radius and height")]
        public void CylinderVolume(CylinderDimensions dimensions)
        {
            var volume = Math.PI * dimensions.Radius * dimensions.Radius * dimensions.Height;
            Console.WriteLine("A cylinder with radius {0} and height {1} has a volume of {2}", dimensions.Radius, dimensions.Height, volume);
        }
    }

    public class CylinderDimensions
    {
        [Description("the radius of the cylinder, in arbitrary units")]
        public double Radius { get; set; }

        [Description("the height of the cylinder, in arbitrary units")]
        public double Height { get; set; }
    }
}
