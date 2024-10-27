// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

namespace IPAddress;

class Program {
    public static void Main(string[] args) {
        Console.WriteLine("Hello World");
        IPv4Address address = new("192.168.10.1/24");
        Console.WriteLine(address.GetAddress());
        Console.WriteLine(address.GetAddressAsInt());

         // 3232238100

        IPv4Address address1 = new(3232238100, 24);
        Console.WriteLine(address1.GetAddress());
        Console.WriteLine(address1.GetCIDR());

        IPv4Address network_address = address1.GetNetworkAddress();
        Console.WriteLine(network_address.GetAddress());
        //Console.WriteLine(network_address.GetAddress());
    }
}

