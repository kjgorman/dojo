namespace Circuitry
{
    public class Wire
    {
        private bool _signal = false;

        public void SetSignal(bool signal)
        {
            _signal = signal;
        }

        public bool GetSignal()
        {
            return _signal;
        }
    }
}
