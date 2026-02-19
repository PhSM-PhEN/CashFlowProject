using System.Collections;
using System.Globalization;

namespace WebApi.Test.InlineData
{
    public class CultureInfoInLineData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "en" };
            yield return new object[] { "pt-BR" };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
