namespace Jeff.ExpenseTracker.Core.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public string[] Errors { get; set; }
    }
}
