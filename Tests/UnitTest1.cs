namespace Tests;

using IPAddress;
using System.Collections.Generic;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        IPv4Address address = new("192.168.10.1/24");

        // Act
        string address_str = address.GetAddress();
        int cidr = address.GetCIDR();

        // Assert
        Assert.Equal("192.168.10.1", address_str);
        Assert.Equal(24, cidr);

    }

    [Fact]
    public void TestIsNetworkAddress() {
        // Arrange
        List<IPv4Address> addresses = [];

        // Act
        addresses.Add(new IPv4Address("192.168.10.0/24"));
        addresses.Add(new IPv4Address("172.16.0.0/16"));
        addresses.Add(new IPv4Address("10.0.0.0/8"));
        addresses.Add(new IPv4Address("192.168.24.0/24"));

        // Assert
        foreach (IPv4Address address in addresses) {
            Assert.True(address.IsNetworkAddress());
        }
    }

    [Fact]
    public void TestGetBroadcastAddress() {
        IPv4Address address = new("192.168.10.0/24");
        
        string broadcast = "192.168.10.255";

        Assert.Equal(broadcast, address.GetBroadcastAddress().GetAddress());
    }
}