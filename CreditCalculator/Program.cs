using System.Threading.Channels;

namespace CreditCalculator
{
    internal static class Program
    {
        public static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("No arguments passed");
                return;
            }

            var dict = Parser.ParseArguments(args);

            dict.TryGetValue("type", out var type);
            dict.TryGetValue("loaninterest", out var loanInterest);
            dict.TryGetValue("numberofmonths", out var numberOfMonths);
            dict.TryGetValue("principal", out var principal);
            dict.TryGetValue("monthpayment", out var monthPayment);

            var creditCalculator = new CreditCalculator(loanInterest, principal, numberOfMonths, monthPayment);

            switch (type)
            {
                case 1 when (loanInterest != 0 && numberOfMonths != 0 && principal != 0):
                    creditCalculator.CalculateDiff();
                    break;
                case 2 when (loanInterest != 0 && principal != 0 && numberOfMonths != 0):
                    creditCalculator.CalculateAnnuityPayment();
                    break;
                case 2 when (loanInterest != 0 && monthPayment != 0 && principal != 0):
                    creditCalculator.CalculateAnnuityNumberOfMonths();
                    break;
                case 2 when (loanInterest != 0 && monthPayment != 0 && numberOfMonths != 0):
                    creditCalculator.CalculateAnnuityPrincipal();
                    break;
                default:
                    Console.WriteLine("Wrong arguments passed!");
                    Console.WriteLine("Have to be like this:");
                    Console.WriteLine("Difference: type=1 loanInterest=(number) numberOfMonths=(number) principal=(number)");
                    Console.WriteLine("Annuity Payment: type=2 loanInterest=(number) numberOfMonths=(number) principal=(number)");
                    Console.WriteLine("Annuity Number of months: type=2 loanInterest=(number) monthPayment=(number) principal=(number)");
                    Console.WriteLine("Annuity Principal: type=2 loanInterest=(number) monthPayment=(number) numberOfMonths=(number)");
                    break;
            }
        }

    }
}