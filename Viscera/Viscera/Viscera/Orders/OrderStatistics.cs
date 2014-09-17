namespace Viscera.Orders
{
    public class OrderStatistics
    {
        public int NumberRed { get; private set; }
        public int NumberGreen { get; private set; }
        public int NumberBlue { get; private set; }

        public OrderStatistics(int numberRed, int numberGreen, int numberBlue)
        {
            NumberRed = numberRed;
            NumberGreen = numberGreen;
            NumberBlue = numberBlue;
        }
    }
}
