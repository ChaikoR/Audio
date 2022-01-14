using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared.Model
{
    public class AddButtonModel
    {
        public int PostId { get; set; }
        public string OperationType { get; set; } = string.Empty;
    }
}
