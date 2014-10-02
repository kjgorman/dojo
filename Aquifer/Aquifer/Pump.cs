namespace Aquifer
{
    public class Pump
    {
        internal void Extract(SumpResevoir value)
        {
            // TODO this should somehow stop the overflow sensor from triggering
            //
            // maybe I should adjust the state of this weird singleton sump resevoir
            // thing... or something?
        }

        internal void ReleaseBackPressure(SumpResevoir value)
        {
            // TODO this should somehow stop the underflow sensor from triggering
        }
    }
}
