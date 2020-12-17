using UnityFlux.Example.Stores;

namespace UnityFlux.Example
{
    public class StoreRepository
    {
        public static StoreRepository Instance;
        public MemoStore MemoStore { get; }
        public MemoMetadataStore MemoMetadataStore { get; }

        public StoreRepository(MemoStore memoStore, MemoMetadataStore memoMetadataStore)
        {
            Instance = this;
            MemoStore = memoStore;
            MemoMetadataStore = memoMetadataStore;
        }
    }
}
