namespace GenericCore.Constants
{
    public static class ErrorMessages
    {
        public static string ToMessage(this ErrorCodes code)
        {
            return code switch
            {
                // Add custom messages here

                // code.ErrorCase => "Some Message";

                _ => code.ToString(),
            };
        }
    }
}
