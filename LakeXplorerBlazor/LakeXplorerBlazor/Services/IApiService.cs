namespace LakeXplorerBlazor
{
    public interface IApiService
    {
        Task<T> HttpGET<T>(string url);
        Task<T> HttpPOST<T>(string url, object postData);
    }
}
