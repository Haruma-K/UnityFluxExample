using System;

namespace UnityFlux.Example.Domain
{
    public class Memo
    {
        public Memo()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public MemoMetadata Metadata { get; set; }
        public string Contents { get; set; }
    }
}