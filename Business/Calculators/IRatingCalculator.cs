using Data.Models;

namespace Business.Calculators
{
    public interface IRatingCalculator
    {
        public double GetTotal(Rating rating);
    }
}
