namespace AssemblyCSharp.Assets.Code.Core.General.Extensions
{
    public static class UtilityExtensions
    {
        public static bool IsWithinRange(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
    }
}
