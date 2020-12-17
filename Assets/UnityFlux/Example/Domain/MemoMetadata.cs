using System;

namespace UnityFlux.Example.Domain
{
    public class MemoMetadata
    {
        public MemoMetadata()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string MemoId { get; set; }
        public string Title { get; set; }
    }
}