namespace BrewCoffee
{
    public interface IDateTimeProvider
    {
        DateTime SystemDateTime { get; }
    }

    public class DateTimeService : IDateTimeProvider
    {
        public DateTime SystemDateTime => DateTime.Now;
    }

    public enum CoffeeStatus
    {
        Ready = 200,
        NoCoffee = 503,
        Unavailable = 418
    }

    public class BrewCoffeeService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private int _ctr = 0;

        public BrewCoffeeService(IDateTimeProvider provider)
        {
            _dateTimeProvider = provider;
        }

        public CoffeeStatus CheckMachineStatus()
        {
            if(_dateTimeProvider.SystemDateTime.Month == 4 && _dateTimeProvider.SystemDateTime.Day == 1)
                return CoffeeStatus.Unavailable;

            _ctr++;

            if(_ctr % 5 == 0)
                return CoffeeStatus.NoCoffee;

            return CoffeeStatus.Ready;
        }
    }
}
