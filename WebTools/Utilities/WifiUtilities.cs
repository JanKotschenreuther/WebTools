namespace WebTools;

public static class WifiUtilities
{
    private const char _escapeCharacter = '\\';
    private static readonly char[] _charactersToEscape = [_escapeCharacter, ';', ',', '"', ':'];

    public static string EncodeWifiConfig(WifiEncryption encryption, string networkSsid, string? password, bool isNetworkSsidHidden)
    {
        List<string> parameters = [$"T:{EncryptionToString(encryption)}"];

        if (String.IsNullOrWhiteSpace(networkSsid) == false)
        {
            parameters.Add($"S:{Escape(networkSsid)}");
        }

        if (encryption != WifiEncryption.None
            && String.IsNullOrWhiteSpace(password) == false
            )
        {
            parameters.Add($"P:{Escape(password)}");
        }

        if (isNetworkSsidHidden)
        {
            parameters.Add($"H:true");
        }

        #region currently not used
        //Lookup for future implementation => https://github.com/zxing/zxing/wiki/Barcode-Contents#wi-fi-network-config-android-ios-11

        //if (String.IsNullOrWhiteSpace(eapMethod) == false)
        //{
        //    parameters.Add($"E:{Escape(eapMethod)}");
        //}

        //if (String.IsNullOrWhiteSpace(anonymousIdentity) == false)
        //{
        //    parameters.Add($"A:{Escape(anonymousIdentity)}");
        //}

        //if (String.IsNullOrWhiteSpace(identity) == false)
        //{
        //    parameters.Add($"I:{Escape(identity)}");
        //}

        //if (String.IsNullOrWhiteSpace(phase2Method) == false)
        //{
        //    parameters.Add($"PH2:{Escape(phase2Method)}");
        //}

        #endregion currently not used

        return $"WIFI:{String.Join(";", parameters)};;";
    }

    private static string EncryptionToString(WifiEncryption encryption) => encryption switch
    {
        WifiEncryption.None => "nopass",
        _ => encryption.ToString(),
    };

    private static string Escape(string input)
    {
        foreach (var character in _charactersToEscape)
        {
            input = input.Replace(character.ToString(), $"\\{character}");
        }

        return input;
    }
}

public enum WifiEncryption
{
    None,
    WEP,
    WPA,
}
