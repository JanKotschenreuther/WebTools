using OtpNet;
using System.Text;

namespace WebTools;

public static class TotpUtilities
{
    public static TotpEntry[] CreateTotpEntries(int period = 30)
    {
        List<TotpEntry> entries = new();
        if (period != 30 && period != 60)
        {
            entries.Add(new TotpEntry()
            {
                Period = period,
                Algorithm = Algorithm.SHA1,
            });
            entries.Add(new TotpEntry()
            {
                Period = period,
                Algorithm = Algorithm.SHA256,
            });
            entries.Add(new TotpEntry()
            {
                Period = period,
                Algorithm = Algorithm.SHA512,
            });
        }

        period = 30;
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA1,
        });
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA256,
        });
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA512,
        });

        period = 60;
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA1,
        });
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA256,
        });
        entries.Add(new TotpEntry()
        {
            Period = period,
            Algorithm = Algorithm.SHA512,
        });

        return entries.ToArray();
    }

    public static void UpdateTotpEntries(string secret, IEnumerable<TotpEntry> entries)
    {
        var secretBytes = Encoding.UTF8.GetBytes(secret);

        foreach (var entry in entries)
        {
            var otpHashMode = toOtpHashMode(entry.Algorithm);
            OtpHashMode toOtpHashMode(Algorithm algorithm) => algorithm switch
            {
                Algorithm.SHA1 => OtpHashMode.Sha1,
                Algorithm.SHA256 => OtpHashMode.Sha256,
                Algorithm.SHA512 => OtpHashMode.Sha512,
                _ => throw new ArgumentOutOfRangeException(nameof(algorithm)),
            };

            entry.Totp6 = new Totp(secretBytes, entry.Period, otpHashMode, 6).ComputeTotp();
            entry.Totp8 = new Totp(secretBytes, entry.Period, otpHashMode, 8).ComputeTotp();
            entry.Countdown = entry.Period - (DateTimeOffset.UtcNow.ToUnixTimeSeconds() % entry.Period);
        }
    }
}

public class TotpEntry
{
    public string Totp6 { get; set; } = String.Empty;
    public string Totp8 { get; set; } = String.Empty;
    public Algorithm Algorithm { get; set; }
    public int Period { get; set; }
    public long Countdown { get; set; }
}

public enum Algorithm
{
    SHA1,
    SHA256,
    SHA512,
}

public enum Digits
{
    digits_6 = 6,
    digits_8 = 8,
}
