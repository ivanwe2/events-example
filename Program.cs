using EventExample.Abstractions;
using EventExample.Models;

namespace EventExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            IDevice device = new Device();

            device.RunDevice();

            Console.ReadKey();
        }
    }
}
