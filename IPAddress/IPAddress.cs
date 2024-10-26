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
            address_as_int = AddressToInt(address);
        }
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

    private static string[] Exploded(string address) {
        return address.Split('.');
    }

    private uint AddressToInt(string address) {
        string[] exploded = Exploded();

        return (uint.Parse(exploded[0]) << 24) + (uint.Parse(exploded[1]) << 16) + (uint.Parse(exploded[2]) << 8) + uint.Parse(exploded[3]);
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
}
