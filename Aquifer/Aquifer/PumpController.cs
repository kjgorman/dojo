namespace Aquifer
{
    public class PumpController
    {
        private readonly MethaneSensor   _methane;
        private readonly AirflowSensor   _airflow;
        private readonly MonoxideSensor  _monoxide;
        private readonly OverflowSensor  _overflow;
        private readonly UnderflowSensor _underflow;

        private readonly Pump  _pump;
        private readonly Alarm _alarm;

        public PumpController()
        {
            _methane   = new MethaneSensor();
            _monoxide  = new MonoxideSensor();
            _airflow   = new AirflowSensor();
            _overflow  = new OverflowSensor();
            _underflow = new UnderflowSensor();

            _pump = new Pump();
            _alarm = new Alarm();
        }

        public void Control()
        {
            while (true)
            {
                var methaneCritical   = _methane.Sense();
                var airflowCritical   = _airflow.Sense();
                var monoxideCritical  = _monoxide.Sense();
                var overflowCritical  = _overflow.Sense();
                var underflowCritical = _underflow.Sense();

                if (!methaneCritical && overflowCritical)
                {
                    //TODO hmm what will happen when methane is critical so we can't
                    //     run the pump, and the water is rising?
                    _pump.Extract(SumpResevoir.Instance.Value);
                }

                if (!methaneCritical && underflowCritical)
                {
                    //TODO hmm what will happen when the methane is critical and we
                    //     aren't able to stop pumping water out of the system?
                    _pump.ReleaseBackPressure(SumpResevoir.Instance.Value);
                }

                if (methaneCritical || airflowCritical || monoxideCritical)
                {
                    _alarm.Sound();
                }
            }
        }
    }
}
