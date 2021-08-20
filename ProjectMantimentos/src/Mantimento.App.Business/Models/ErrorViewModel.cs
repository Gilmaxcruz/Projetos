using System;

namespace Mantimentos.App.Business.Models
{
    public class ErrorViewModela
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
