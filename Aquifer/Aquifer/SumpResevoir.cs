using System;

namespace Aquifer
{
    internal class SumpResevoir
    {
        public static readonly Lazy<SumpResevoir> Instance = new Lazy<SumpResevoir>(() => new SumpResevoir());

        private static T PerformUnsafeIO<T>()
        {
            throw new AnnoyingObtuseIoMisconfigurationInYourTestingEnvironmentException();
        }

        public bool GetVolume()
        {
            return PerformUnsafeIO<bool>();
        }

        public int GetMethaneLevelAsPercentage()
        {
            return PerformUnsafeIO<int>();
        }

        public int GetMonoxideLevelAsPercentage()
        {
            return PerformUnsafeIO<int>();
        }

        public int GetAirflowAsSignedDeviationFromMean()
        {
            return PerformUnsafeIO<int>();
        }

        private class AnnoyingObtuseIoMisconfigurationInYourTestingEnvironmentException : Exception
        {
            public override string Message
            {
                get { return 
@"we don't have a spare sump resevoir you can just arbitrarily fill with water or methane or whatever...
  you're going to have to externalise, or hexagonalise, or otherwise ise-ify this guy"; }
            }
        }
    }
}
