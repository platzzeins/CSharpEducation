using CoffeeMachine.Core;
using CoffeeMachine.Types;
using CoffeeMachine.UI;

namespace CoffeeMachine
{
    internal static class Program
    {
        public static void Main()
        {
            var storage = MachineStorage.Load();
            var machineCore = new MachineCore(storage);
            var machineUi = new MachineUi(machineCore, machineCore.Storage);

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