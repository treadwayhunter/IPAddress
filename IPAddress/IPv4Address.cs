namespace IPAddress;
public class IPv4Address {
    private readonly string address;
    private readonly uint address_as_int;
    private readonly int cidr;

    public IPv4Address(string address_w_cidr) {
        // check that address is of type string, and that it contains a cidr
        string[] arr = address_w_cidr.Split('/');

        if (arr.Length != 2) {
            // return an error
            Console.WriteLine("ERROR! IP ADDRESS WRONG");
            address = "0.0.0.0";
            address_as_int = 0;
            cidr = 0;
        }
        else {
            address = ValidateStringAddress(arr[0]);
            cidr = ValidateCIDR(int.Parse(arr[1])); // this could fail too.
            address_as_int = AddressToUint(address);
        }
    }

    public IPv4Address(uint address_as_int, int cidr) {
        // todo
        this.address_as_int = address_as_int;
        this.cidr = ValidateCIDR(cidr);
        address = UintToAddress(this.address_as_int);
    }

    public string GetAddress() {
        return address;
    }

    public uint GetAddressAsInt() {
        return address_as_int;
    }

    public int GetCIDR() {
        return cidr;
    }

    public string GetSubnetMask() {
        return CIDRToSubnetMask();
    }

    public string[] Exploded() {
        return Exploded(address);
    }

    public IPv4Address GetNetworkAddress() {
        string subnet_mask = CIDRToSubnetMask();
        uint subnet_mask_as_int = AddressToUint(subnet_mask);

        uint result = address_as_int & subnet_mask_as_int;

        return new IPv4Address(result, cidr);
    }

    public IPv4Address GetBroadcastAddress() {
        string wildcard_mask = CIDRToWildcardMask();
        uint wildcard_mask_as_int = AddressToUint(wildcard_mask);

        uint result = address_as_int | wildcard_mask_as_int;

        return new IPv4Address(result, cidr);
    }

    public bool IsNetworkAddress() {
        string subnet_mask = CIDRToSubnetMask();
        uint subnet_mask_as_int = AddressToUint(subnet_mask);

        uint result = address_as_int & subnet_mask_as_int; 

        if (result == address_as_int) {
            return true;
        }

        return false;
    }

    public bool IsBroadcastAddress() {
        string wildcard_mask = CIDRToWildcardMask();
        uint wildcard_mask_as_int = AddressToUint(wildcard_mask);

        uint result = address_as_int | wildcard_mask_as_int;

        if (result == address_as_int) {
            return true;
        }

        return false;
    }

    private static string[] Exploded(string address) {
        return address.Split('.');
    }

    private static uint AddressToUint(string address) {
        string[] exploded = Exploded(address); // use the private one, oops

        return (uint.Parse(exploded[0]) << 24) + (uint.Parse(exploded[1]) << 16) + (uint.Parse(exploded[2]) << 8) + uint.Parse(exploded[3]);
    }

    private static string UintToAddress(uint address_as_int) {
        uint octet1 = (address_as_int >> 24) & 0xFF;
        uint octet2 = (address_as_int >> 16) & 0xFF;
        uint octet3 = (address_as_int >> 8) & 0xFF;
        uint octet4 = address_as_int & 0xFF;

        return $"{octet1}.{octet2}.{octet3}.{octet4}";
    }

    private static string ValidateStringAddress(string address) {
        string[] exploded = Exploded(address);

        if(exploded.Length != 4) return "0.0.0.0"; // throw an error

        foreach(string octet in exploded) {
            if (int.Parse(octet) > 255 || int.Parse(octet) < 0) {
                // throw an error
                return "0.0.0.0";
            }
        }

        return address;
    }

    private static int ValidateCIDR(int cidr) {
        if (cidr > 32 || cidr < 0) {
            return 0; // throw error
        }
        return cidr;
    }

    private string CIDRToSubnetMask() {
        return cidr switch
        {
            0 => "0.0.0.0",
            1 => "128.0.0.0",
            2 => "192.0.0.0",
            3 => "224.0.0.0",
            4 => "240.0.0.0",
            5 => "248.0.0.0",
            6 => "252.0.0.0",
            7 => "254.0.0.0",
            8 => "255.0.0.0",
            9 => "255.128.0.0",
            10 => "255.192.0.0",
            11 => "255.224.0.0",
            12 => "255.240.0.0",
            13 => "255.248.0.0",
            14 => "255.252.0.0",
            15 => "255.254.0.0",
            16 => "255.255.0.0",
            17 => "255.255.128.0",
            18 => "255.255.192.0",
            19 => "255.255.224.0",
            20 => "255.255.240.0",
            21 => "255.255.248.0",
            22 => "255.255.252.0",
            23 => "255.255.254.0",
            24 => "255.255.255.0",
            25 => "255.255.255.128",
            26 => "255.255.255.192",
            27 => "255.255.255.224",
            28 => "255.255.255.240",
            29 => "255.255.255.248",
            30 => "255.255.255.252",
            31 => "255.255.255.254",
            32 => "255.255.255.255",
            _ => "0.0.0.0",
        };
    }

    private string CIDRToWildcardMask() {
        return cidr switch
        {
            0 => "255.255.255.255",
            1 => "127.255.255.255",
            2 => "63.255.255.255",
            3 => "31.255.255.255",
            4 => "15.255.255.255",
            5 => "7.255.255.255",
            6 => "3.255.255.255",
            7 => "1.255.255.255",
            8 => "0.255.255.255",
            9 => "0.127.255.255",
            10 => "0.63.255.255",
            11 => "0.31.255.255",
            12 => "0.15.255.255",
            13 => "0.7.255.255",
            14 => "0.3.255.255",
            15 => "0.1.255.255",
            16 => "0.0.255.255",
            17 => "0.0.127.255",
            18 => "0.0.63.255",
            19 => "0.0.31.255",
            20 => "0.0.15.255",
            21 => "0.0.7.255",
            22 => "0.0.3.255",
            23 => "0.0.1.255",
            24 => "0.0.0.255",
            25 => "0.0.0.127",
            26 => "0.0.0.63",
            27 => "0.0.0.31",
            28 => "0.0.0.15",
            29 => "0.0.0.7",
            30 => "0.0.0.3",
            31 => "0.0.0.1",
            32 => "0.0.0.0",
            _ => "255.255.255.255",
        };
    }
}
