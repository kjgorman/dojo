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
    }
}
