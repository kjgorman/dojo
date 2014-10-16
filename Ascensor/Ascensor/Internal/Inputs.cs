using System.Collections.Generic;
using System.Linq;
using Ascensor.Machinery.Lang;

namespace Ascensor.Internal
{
    public interface ElevatorInput
    {
    }

    public class Request : ElevatorInput
    {
        public uint Floor;

        public static IEnumerable<ElevatorInput> AllFloors =
            Enumerable.Range(0, 2).Select(floor => new Request { Floor = (uint) floor });
    }

    public class Movement : ElevatorInput
    {
        public static IEnumerable<ElevatorInput> Movements =
            Enumerables.Of(new Movement());
    }

    public class Timeout : ElevatorInput
    {
        public static IEnumerable<ElevatorInput> Timeouts =
            Enumerables.Of(new Timeout());
    }

    public class Arrival : ElevatorInput
    {
        public static IEnumerable<ElevatorInput> Arrivals =
            Enumerables.Of(new Arrival());
    }
}
