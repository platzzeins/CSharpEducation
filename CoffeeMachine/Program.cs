using CoffeeMachine.Core;
using CoffeeMachine.UI;

namespace CoffeeMachine
{
    internal static class Program
    {
        public static void Main()
        {
            var machineCore = new MachineCore();
            var machineUi = new MachineUi(machineCore);
            machineCore.Notify += machineUi.PrintState;

            while (true)
            {
                if (!machineUi.GeneralMenu())
                {
                    break;
                }  
            }
        }
    }
}