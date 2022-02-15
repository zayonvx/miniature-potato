using OnePlan.Business.Enums;

namespace OnePlan.Business.ResponseModels;

public class Response<T> : BaseResponse 
{
    public Response(T result) : base(ResponseType.Success)
    {
        Data = result;
    }
    public T Data { get; set; }
}
