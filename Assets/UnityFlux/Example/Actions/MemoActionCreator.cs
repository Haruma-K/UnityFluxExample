using System;
using System.Linq;
using System.Threading.Tasks;
using UnityFlux.Core;
using UnityFlux.Example.Api;

namespace UnityFlux.Example.Actions
{
    public class MemoActionCreator
    {
        private readonly IDispatcher _dispatcher;
        private readonly IMemoApiService _memoApiService;

        public MemoActionCreator(IDispatcher dispatcher, IMemoApiService memoApiService)
        {
            _dispatcher = dispatcher;
            _memoApiService = memoApiService;
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            // メタデータを取得する
            await FetchMetadataListAsync();
            // 最初のメモを編集中メモに設定する
            var firstItem = StoreRepository.Instance.MemoMetadataStore.Items.First();
            await ChangeEditingMemoAsync(firstItem.Key);
        }

        /// <summary>
        /// メモのメタデータ一覧を取得する。
        /// </summary>
        /// <returns></returns>
        public async Task FetchMetadataListAsync()
        {
            try
            {
                var startIndex = StoreRepository.Instance.MemoStore.Items.Count;
                var metaDataList = await _memoApiService.GetMetadataList(startIndex, 5);
                _dispatcher.Dispatch(new Payload(MemoActionTypes.FetchAllMetadata, metaDataList));
            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new Payload(MemoActionTypes.Failure, ex.Message));
                throw;
            }
        }

        /// <summary>
        /// 編集中のメモを切り替える。
        /// </summary>
        /// <param name="memoId"></param>
        /// <returns></returns>
        public async Task ChangeEditingMemoAsync(string memoId)
        {
            try
            {
                // 無かったら取得する
                if (!StoreRepository.Instance.MemoStore.Items.TryGetValue(memoId, out var memo))
                {
                    memo = await _memoApiService.GetMemo(memoId);
                }
                
                _dispatcher.Dispatch(new Payload(MemoActionTypes.ChangeEditingMemo, memo));
            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new Payload(MemoActionTypes.Failure, $"{ex.Message}{Environment.NewLine}{ex.StackTrace}"));
                throw;
            }
        }

        /// <summary>
        /// タイトルを更新する。
        /// </summary>
        /// <param name="memoId"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task ChangeTitle(string memoId, string title)
        {
            try
            {
                await _memoApiService.UpdateTitle(memoId, title);
                _dispatcher.Dispatch(new Payload(MemoActionTypes.UpdateTitle, (memoId, title)));
            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new Payload(MemoActionTypes.Failure, ex.Message));
                throw;
            }
        }

        /// <summary>
        /// 内容を更新する。
        /// </summary>
        /// <param name="memoId"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public async Task ChangeContents(string memoId, string contents)
        {
            try
            {
                await _memoApiService.UpdateContents(memoId, contents);
                _dispatcher.Dispatch(new Payload(MemoActionTypes.UpdateContents, (memoId, contents)));
            }
            catch (Exception ex)
            {
                _dispatcher.Dispatch(new Payload(MemoActionTypes.Failure, ex.Message));
                throw;
            }
        }
    }
}