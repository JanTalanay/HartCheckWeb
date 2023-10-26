using System.Text.RegularExpressions;

namespace Hart_Check_Official.Helper
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            // Slugify value
            if (value == null) return null;
            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "\\$1-\\$2").ToLower();
        }
    }
}
