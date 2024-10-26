// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

namespace IPAddress;

class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Hello World");
        IPv4Address address = new("192.168.10.1/24");
        Console.WriteLine(address.GetAddress());
    }
}

