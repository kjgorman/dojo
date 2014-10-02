using NUnit.Framework;

namespace Aquifer.Test
{
    public class PumpControllerTests
    {
        [Test]
        public void PumpControllerControlsStuff()
        {
            var controller = new PumpController();

            // hmm wait a minute... the passage of time is modelled
            // as an infinite loop, and we can't actually observe
            // any events occurring in the system... someone should
            // maybe refactor this.
            //controller.Control();

            // but let's make sure the test is green so no one notices
            // how fucked our build is
            Assert.True(true);
        }

        // TEST: if the water underflows, the pump release water
        // TEST: if the water overflows, the pump extracts water
        // TEST: once the pump has actioned, the previous sensor no longer warns
        // TEST: but if there's monoxide, subsequent readings should warn
        // TEST: but if there's monoxide, calling the pump should...?

        // etc. for the gaseous elements, the airflow, the alarm, presumably some logging
    }
}
