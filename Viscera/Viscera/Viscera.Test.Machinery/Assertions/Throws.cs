using System;

namespace Viscera.Test.Machinery.Assertions
{
    public partial class Assertions
    {
        public static T Throws<T>(Action block) where T : Exception
        {
            var type = typeof(T);

            try
            {
                block();
            }
            catch (T e)
            {
                return e;
            }
            catch (Exception e)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Expected error of type <{0}>, got <{1}>", type, e.GetType());
            }

            throw new Exception("Expected error of type <" + type.FullName + ">, got nothing.");
        }
    }
}
