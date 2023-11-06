using Microsoft.AspNetCore.Mvc.Razor;

namespace ScootersMc.App.Extension
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, string documento)
        {
            return Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");

        }

        public static string FormataTelefone(this RazorPage page, string telefone)
        {
            return Convert.ToUInt64(telefone).ToString(@"(00\) 00000\-0000");
        }

        //public static string FormataData(this RazorPage page, DateTime data)
        //{
        //    return data.ToString(@"00\/00\/0000");
        //}

    }
}
