namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class ErrorResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}
