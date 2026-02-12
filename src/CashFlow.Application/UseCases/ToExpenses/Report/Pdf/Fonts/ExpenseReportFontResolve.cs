using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.ToExpenses.Report.Pdf.Fonts
{
    public class ExpenseReportFontsResolver : IFontResolver
    {
        public byte[]? GetFont(string faceName)
        {
            var stream = ReadFontFile(faceName);

            stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

            var length = stream!.Length;

            var data = new byte[length];

            stream.Read(buffer: data, offset: 0, count: (int)length);
            return data;
        }

        public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
        {
            return new FontResolverInfo(familyName);
        }
        private static Stream? ReadFontFile(string faceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetManifestResourceStream($"CashFlow.Application.UseCases.ToExpenses.Report.Pdf.Fonts.{faceName}.ttf");
        }
    }
}
