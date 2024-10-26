namespace Tests;

using IPAddress;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

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
}