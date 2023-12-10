namespace LakeXplorerBlazor
{
    public interface IApiService
    {
        Task<T> HttpGET<T>(string url);
        Task<T> HttpPOST<T>(string url, object postData);
        Task<bool> UpdateLikeStatus(int lakeId, bool isLiked);
    }
}
