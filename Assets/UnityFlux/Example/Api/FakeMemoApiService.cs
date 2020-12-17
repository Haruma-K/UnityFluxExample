using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityFlux.Example.Domain;

namespace UnityFlux.Example.Api
{
    public class FakeMemoApiService : IMemoApiService
    {
        private readonly List<Memo> _memos = new List<Memo>();
        private readonly Dictionary<string, Memo> _memoDict = new Dictionary<string, Memo>();

        public FakeMemoApiService()
        {
            for (var i = 0; i < 100; i++)
            {
                var memo = new Memo
                {
                    Contents = $"{i}番目のメモです",
                    Metadata = new MemoMetadata
                    {
                        Title = $"memo-{i}"
                    }
                };
                memo.Metadata.MemoId = memo.Id;
                _memos.Add(memo);
                _memoDict.Add(memo.Id, memo);
            }
        }

        public Task<Memo> GetMemo(string id)
        {
            return Task.FromResult(_memoDict[id]);
        }

        public Task<MemoMetadata[]> GetMetadataList(int startIndex, int count)
        {
            var metaDataList = new List<MemoMetadata>();
            for (var i = startIndex; i < startIndex + count; i++)
            {
                if (_memos.Count - 1 < i)
                {
                    break;
                }

                metaDataList.Add(_memos[i].Metadata);
            }

            return Task.FromResult(metaDataList.ToArray());
        }
        
        public Task UpdateTitle(string id, string title)
        {
            _memoDict[id].Metadata.Title = title;
            return Task.CompletedTask;
        }
        
        public Task UpdateContents(string id, string contents)
        {
            _memoDict[id].Contents = contents;
            return Task.CompletedTask;
        }
    }
}