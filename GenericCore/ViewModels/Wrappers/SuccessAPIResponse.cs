namespace GenericCore.ViewModels.Wrappers
{
    public class SuccessAPIResponse<T> : APIResponseResult<T>
    {
        public SuccessAPIResponse(T Result)
        {
            this.Status = true;
            this.Result = Result;
        }
    }

    public static class SuccessAPIResponseWrapper
    {
        public static SuccessAPIResponse<T> Wrap<T>(T result) => new SuccessAPIResponse<T>(result);
    }
}
