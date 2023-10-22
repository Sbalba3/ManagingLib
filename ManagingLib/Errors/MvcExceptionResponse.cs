namespace ManagingLib.Errors
{
    public class MvcExceptionResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
        public MvcExceptionResponse(int statusCode,string? message=null,string? details=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Details = details;
            
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request,You Have Made ",
                401 => "Authorized, You Are Not",
                404 => "Resource Was Not Found",
                500 => "Errors Are The Path To The Dark Side. Errors Lead To Anger. Anger Leads To Hate. Hate Leads To Career Change ",
                _ => null

            };
        }
    }
}
