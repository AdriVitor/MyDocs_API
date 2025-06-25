namespace MyDocs.Shared.Validators
{
    public class ExceptionValidator : Exception
    {
        public ExceptionValidator(string error) : base(error){}

        public static void When(bool hasError, string errorMessage)
        {
            if (hasError) 
                throw new Exception(errorMessage);
        }
    }
}
