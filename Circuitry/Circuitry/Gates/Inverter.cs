namespace Circuitry.Gates
{
    public class Inverter
    {
        public Inverter(Wire @in, Wire @out)
        {
            @out.SetSignal(!@in.GetSignal());
        }
    }
}
