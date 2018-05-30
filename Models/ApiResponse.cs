using System;

public class ApiResponse
{
    public bool HasError { get; set; }
    public string ErrorMessage { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime ResponseDate { get; set; }

    public object Data { get; set; }

    public ApiResponse()
    {
        HasError = false;
        ErrorMessage = "";
        RequestDate = DateTime.Now;
    }
}