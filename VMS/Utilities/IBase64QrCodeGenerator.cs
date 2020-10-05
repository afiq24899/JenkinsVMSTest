using System;

namespace Lingkail.VMS.Auth.Web.Utilities
{
    public interface IBase64QrCodeGenerator
    {
        string Generate(Uri target);
    }
}