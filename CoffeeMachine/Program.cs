using CoffeeMachine.Core;
using CoffeeMachine.Types;
using CoffeeMachine.UI;

namespace CoffeeMachine
{
    internal static class Program
    {
        public static void Main()
        {
            var machineStorage = new MachineStorage();
            var machineCore = new MachineCore(machineStorage);
            var machineUi = new MachineUi(machineCore, machineStorage);
            machineCore.Notify += machineUi.PrintState;

            while (true)
            {
                if (machineCore.MachineState == State.Exiting)
                {
                    break;
                }  
                machineUi.GeneralMenu();
            }
        }
    }
}