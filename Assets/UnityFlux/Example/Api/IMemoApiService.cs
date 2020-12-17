using System.Threading.Tasks;
using UnityFlux.Example.Domain;

namespace UnityFlux.Example.Api
{
    public interface IMemoApiService
    {
        Task<Memo> GetMemo(string id);
        Task<MemoMetadata[]> GetMetadataList(int startIndex, int count);
        Task UpdateTitle(string id, string title);
        Task UpdateContents(string id, string contents);
    }
}