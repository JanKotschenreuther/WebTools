using System.Numerics;

namespace ClassLibrary;

public class Math
{
    /// <summary>
    /// Returns the result of remapping of the value from a source range to a target range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="sourceMin"></param>
    /// <param name="sourceMax"></param>
    /// <param name="targetMin"></param>
    /// <param name="targetMax"></param>
    /// <returns></returns>
    public static T Remap<T>(T value, T sourceMin, T sourceMax, T targetMin, T targetMax)
        where T : INumber<T>
    {
        T sourceRange = sourceMax - sourceMin;
        T targetRange = targetMax - targetMin;

        T targetValue = T.Zero;
        if (sourceRange == T.Zero)
        {
            targetValue = targetMin;
        }
        else
        {
            targetValue = (((value - sourceMin) * targetRange) / sourceRange) + targetMin;
        }

        return targetValue;
    }

    /// <summary>
    /// Returns the result of remapping of the value from a source range to a percentage [0..100].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="sourceMin"></param>
    /// <param name="sourceMax"></param>
    /// <returns></returns>
    public static T RemapToPercent<T>(T value, T sourceMin, T sourceMax)
        where T : INumber<T>
    {
        return Remap<T>(value, sourceMin, sourceMax, T.Zero, T.CreateChecked(100));
    }
}
