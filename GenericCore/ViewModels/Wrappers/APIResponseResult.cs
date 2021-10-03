namespace GenericCore.ViewModels.Wrappers
{
    public class APIResponseResult<T> : APIResponse
    {
        public T Result { get; set; }
    }
}
