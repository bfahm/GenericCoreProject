namespace GenericCore.Constants
{
    public static class ErrorMessages
    {
        public static string ToMessage(this ErrorCodes code)
        {
            return code switch
            {
                ErrorCodes.IdentityError => "A list of Identity Errors has occured.",
                ErrorCodes.ExsitingEmail => "User with this email address already exsists.",
                ErrorCodes.ExsitingPhoneNumber => "User with this Phone Number was registered before.",
                ErrorCodes.EmailNotFound => "User with provided email does not exsist.",
                ErrorCodes.IncorrectEmailPasswordCombination => "Email/Password combination is wrong.",
                // Add custom messages here

                // code.ErrorCase => "Some Message";

                _ => code.ToString(),
            };
        }
    }
}
