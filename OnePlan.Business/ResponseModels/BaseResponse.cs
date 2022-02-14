namespace OnePlan.Business.ResponseModels;

public class BaseResponse
{
    public BaseResponse()
    {
        GeneratedAtZulu = DateTime.UtcNow;
    }

    public BaseResponse(ResponseType status) : this()
    {
        Status = status;
    }
}
