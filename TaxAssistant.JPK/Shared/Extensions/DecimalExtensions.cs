namespace TaxAssistant.JPK.Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal? NullifyZero(this decimal input)
        {
            if (input == decimal.Zero)
            {
                return null;
            }

            return input;
        }
    }
}
