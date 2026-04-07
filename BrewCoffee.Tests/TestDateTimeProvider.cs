namespace BrewCoffee.Tests
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public DateTime TestDate { get; set; }
        public DateTime SystemDateTime => TestDate;
    }
}
