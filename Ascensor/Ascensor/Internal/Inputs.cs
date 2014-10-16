using System.Collections.Generic;
using System.Linq;

namespace Ascensor.Internal
{
    public interface ElevatorInput
    {
    }

    public class Request : ElevatorInput
    {
        public uint Floor;

        public static IEnumerable<ElevatorInput> AllFloors =
            Enumerable.Range(0, 3).Select(floor => new Request { Floor = (uint) floor });
    }
}
