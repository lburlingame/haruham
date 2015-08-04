using System.Security.Cryptography;

/// <summary>
/// Summary description for Crypt
/// </summary>
public class Crypt
{

    public static byte[] getNewSalt(int length)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        var random = new byte[length];

        rng.GetBytes(random);

        return random;
    }

    public static string RandomReadableString(int length)
    {
        string charSet = "0123456789ABCDEFGHJKMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz";
        return RandomString(charSet, length);
    }


    // http://musingmarc.blogspot.com/2012/12/how-to-create-random-readable-strings.html
    public static string RandomString(string charSet, int length)
    {
        var rng = new RNGCryptoServiceProvider();
        var random = new byte[length];
        rng.GetNonZeroBytes(random);

        var buffer = new char[length];
        var usableChars = charSet.ToCharArray();
        var usableLength = usableChars.Length;

        for (int index = 0; index < length; index++)
        {
            buffer[index] = usableChars[random[index] % usableLength];
        }

        return new string(buffer);
    }

}