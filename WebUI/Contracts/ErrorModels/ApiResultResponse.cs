namespace ProductManagment.WebUI.Contracts.ErrorModels
{
    public class ApiResultResponse<T>
    {
        //public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; }
        public T? Value { get; set; }
    }

}
