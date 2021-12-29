using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcService.Models
{
    public class Messages
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessagesId { get; set; }
        public string? Name { get; set; }
        public byte[]? BinaryData { get; set; }
    }
}

//https://stackoverflow.com/questions/36432028/how-to-convert-a-file-into-byte-array-in-memory
//https://streletzcoder.ru/organizatsiya-hraneniya-faylov-v-baze-dannyih-microsoft-sql-server-universalnyiy-sposob/
//https://www.codegrepper.com/code-examples/csharp/how+to+convert+iformfile+to+byte+array+c%23
//https://newbedev.com/csharp-convert-iformfile-to-byte-array-asp-net-core-code-example
//https://docs.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0

//https://www.puresourcecode.com/dotnet/blazor/create-an-accordion-component-with-blazor/