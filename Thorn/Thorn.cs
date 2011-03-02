using System.Dynamic;
using System.Linq;
using System.Text;

namespace Thorn
{
    public class Thorn
    {
        public static dynamic GetRouteDictionary(string[] args)
        {
            dynamic result = new ExpandoObject();
            if (args.Length < 1)
            {
                return result;
            }
            
            var firstArg = args.First();

            if(firstArg.Contains(":"))
            {
                result.Namespace = firstArg.Split(':')[0];
                result.MethodName = firstArg.Split(':')[1];
            }
            else
            {
                result.MethodName = firstArg;
            }

            result.Args = args.Skip(1).ToArray();

            return result;
        }

        public static IThornConfigurationHelper ConfigureHandlers()
        {
            return new ThornConfigurationHelper();
        }

        private static IThornConfiguration _configuration;
        public static IThornConfiguration Configuration
        {
            get
            {
                return _configuration = _configuration ?? new ThornConfiguration();
            }
        }
    }
}
