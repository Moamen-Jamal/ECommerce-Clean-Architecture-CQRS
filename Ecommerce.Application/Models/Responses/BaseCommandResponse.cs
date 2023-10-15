using System.Net;

namespace Ecommerce.Application.Models
{
    public class BaseCommandResponse<T>
    {
        public BaseCommandResponse()
        {
            Status = (int)HttpStatusCode.OK;
        }

        public BaseCommandResponse(int Status)
        {
            this.Status = Status;
        }

        public bool Successed { get; set; } = false;
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }
}