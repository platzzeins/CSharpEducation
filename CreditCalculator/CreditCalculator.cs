namespace CreditCalculator;

public class CreditCalculator
{
    private int _loanInterest;
    private readonly int _principal;
    private readonly int _numberOfMonths;
    private readonly int _monthPayment;

    public CreditCalculator(int loanInterest, int principal, int numberOfMonths, int monthPayment)
    {
        _loanInterest = loanInterest;
        _principal = principal;
        _numberOfMonths = numberOfMonths;
        _monthPayment = monthPayment;
    }
    
    
    public void CalculateDiff()
        {

            _loanInterest = _loanInterest / 100 / 12;
            var totalPayments = 0;

            for (var month = 1; month < _numberOfMonths + 1;  month++)
            {
                // ReSharper disable once PossibleLossOfFraction
                var difference = Math.Ceiling((double)(_principal 
                    / _numberOfMonths + _loanInterest
                    // ReSharper disable once PossibleLossOfFraction
                    * (_principal - (_principal * (month - 1)) / _numberOfMonths)));
                totalPayments += (int)difference;
                Console.WriteLine($"Month {month}: MonthPayment is {difference}");
            }

            Console.WriteLine($"\nOverMonthPayment = {totalPayments - _principal}");

        }

        public void CalculateAnnuityPayment()
        {
            _loanInterest = _loanInterest / 100 / 12;

            var monthPayment = _principal * (_loanInterest * Math.Pow(_loanInterest + 1, _numberOfMonths) / 
                                       (Math.Pow(_loanInterest + 1, _numberOfMonths) - 1));
            Console.WriteLine($"Your annuity MonthPayment = {monthPayment}");
            Console.WriteLine($"OverMonthPayment: {Math.Ceiling(monthPayment * _numberOfMonths - _principal)}");
        }

        public void CalculateAnnuityNumberOfMonths()
        {
            _loanInterest = _loanInterest / 100 / 12;

            // ReSharper disable once PossibleLossOfFraction
            var numberOfMonths = Math.Ceiling(Math.Log(_monthPayment / (_monthPayment - _loanInterest * _principal),
                1 + _loanInterest));
            Console.WriteLine(numberOfMonths % 12 != 0
                ? $"It will take {numberOfMonths / 12} years and {numberOfMonths % 12 } months to repay this loan!"
                : $"It will take {numberOfMonths / 12} years");
        }

        public void CalculateAnnuityPrincipal()
        {
            _loanInterest = _loanInterest / 100 / 12;

            var principal = _monthPayment / ((_loanInterest * Math.Pow(1 + _loanInterest, _numberOfMonths)) /
                                       (Math.Pow(1 + _loanInterest, _numberOfMonths) - 1));

            Console.WriteLine($"Your loan Principal = {principal}!");
            Console.WriteLine($"OverMonthPayment: {Math.Ceiling(_numberOfMonths * _monthPayment - principal)}");
        }
        
        
        
}