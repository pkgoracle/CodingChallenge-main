namespace AnimalAg.CodingChallenge.Data.Entities.Common
{
    /// <summary>
    /// CommonResultEntity is a class that represents the standard structure for API responses, encapsulating the success status, error information, and result data. It is designed to provide a consistent format for returning results from service methods, allowing for easy handling of success and error cases in a unified manner.
    /// </summary>
    public class CommonResultEntity
    {
        /// <summary>
        /// IsSuccess indicates whether the operation was successful or not. A value of true means the operation completed successfully, while false indicates that an error occurred during the operation. This property is essential for clients to quickly determine the outcome of their request without needing to inspect error codes or messages in detail.
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// ErrorCode provides a standardized code that represents the specific error that occurred during the operation. This code can be used by clients to identify the type of error and take appropriate actions, such as displaying user-friendly messages or triggering specific error handling logic. The ErrorCode should be defined in a way that allows for easy mapping to known error conditions within the application.
        /// </summary>
        public string? ErrorCode { get; set; }
        /// <summary>
        /// ErrorMessage contains a human-readable description of the error that occurred during the operation. This message is intended to provide additional context and details about the error, which can be useful for debugging and troubleshooting purposes. It should be clear and concise, helping developers and users understand what went wrong without needing to refer to external documentation or error code definitions.
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets the result value associated with the operation.
        /// </summary>
        public object? Result { get; set; }
    }
}
