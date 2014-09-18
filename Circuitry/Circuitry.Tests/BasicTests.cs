using Circuitry.Gates;
using NUnit.Framework;

namespace Circuitry.Tests
{
    public class BasicTests
    {
        [Test]
        public void WiresStartWithNoSignal()
        {
            var signal = new Wire().GetSignal();
            Assert.IsFalse(signal);
        }

        [Test]
        public void InvertersInvertSignals()
        {
            var @in = new Wire();
            var @out = new Wire();

            new Inverter(@in, @out);

            var signal = @out.GetSignal();

            Assert.IsTrue(signal);
        }

        [Test]
        public void InvertersInvertOtherInverters()
        {
            var first  = new Wire();
            var second = new Wire();
            var third  = new Wire();

            new Inverter(first, second);
            new Inverter(second, third);

            var signal = third.GetSignal();

            Assert.IsFalse(signal);
        }
    }
}
