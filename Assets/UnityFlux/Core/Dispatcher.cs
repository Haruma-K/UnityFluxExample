using System;
using System.Collections.Generic;

namespace UnityFlux.Core
{
    public class Dispatcher : IDispatcher
    {
        private readonly Dictionary<string, Action<Payload>> _callbacks = new Dictionary<string, Action<Payload>>();
        private readonly Dictionary<string, bool> _processedMap = new Dictionary<string, bool>();
        private readonly Dictionary<string, bool> _processingMap = new Dictionary<string, bool>();
        private Payload _dispatchingPayload;

        public bool IsDispatching { get; private set; }

        /// <summary>
        ///     コールバックを登録する。
        /// </summary>
        /// <param name="callback"></param>
        /// <returns>識別子</returns>
        public string Register(Action<Payload> callback)
        {
            var callbackId = Guid.NewGuid().ToString();
            _callbacks[callbackId] = callback;
            _processedMap[callbackId] = false;
            _processingMap[callbackId] = false;
            return callbackId;
        }

        /// <summary>
        ///     コールバックの登録を解除する
        /// </summary>
        /// <param name="dispatchToken"></param>
        public bool Unregister(string dispatchToken)
        {
            var success = true;
            success &= _callbacks.Remove(dispatchToken);
            success &= _processedMap.Remove(dispatchToken);
            success &= _processingMap.Remove(dispatchToken);
            return success;
        }

        /// <summary>
        ///     全てのコールバックを実行する。
        /// </summary>
        /// <param name="payload"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Dispatch(Payload payload)
        {
            if (IsDispatching)
            {
                throw new InvalidOperationException("The dispatcher is already working.");
            }

            try
            {
                foreach (var id in _callbacks.Keys)
                {
                    _processingMap[id] = false;
                    _processedMap[id] = false;
                }

                _dispatchingPayload = payload;
                IsDispatching = true;

                // Dispatcherは必ずすべてのコールバックを実行する
                foreach (var dispatchToken in _callbacks.Keys)
                {
                    // 処理中だったらスキップ
                    if (_processingMap[dispatchToken])
                    {
                        continue;
                    }

                    // コールバックを実行する
                    ProcessCallback(dispatchToken);
                }
            }
            finally
            {
                _dispatchingPayload = null;
                IsDispatching = false;
            }
        }

        /// <summary>
        ///     引数のコールバックIDを持つコールバックの終了を待機する。
        /// </summary>
        /// <param name="dispatchTokens"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void WaitFor(params string[] dispatchTokens)
        {
            if (!IsDispatching)
            {
                throw new InvalidOperationException("The dispatcher is already working.");
            }

            foreach (var dispatchToken in dispatchTokens)
            {
                if (_processingMap[dispatchToken])
                {
                    // 待とうとしている対象が処理中かつ処理終了していない = 待機中
                    // お互い待ち合ってしまっている -> 例外
                    if (!_processedMap[dispatchToken])
                    {
                        throw new InvalidOperationException(
                            $"Circular references by {nameof(WaitFor)} have been detected.");
                    }

                    // 既に処理済みだったら処理しない
                    continue;
                }

                // 未処理のコールバックだったら処理する
                ProcessCallback(dispatchToken);
            }
        }

        /// <summary>
        ///     コールバックが登録されているか。
        /// </summary>
        /// <param name="dispatchToken"></param>
        /// <returns></returns>
        public bool IsRegistered(string dispatchToken)
        {
            return _callbacks.ContainsKey(dispatchToken);
        }

        /// <summary>
        ///     現在処理中の<see cref="Payload" />を<see cref="dispatchToken" />に対応するコールバックに渡して実行する。
        /// </summary>
        /// <param name="dispatchToken"></param>
        private void ProcessCallback(string dispatchToken)
        {
            // 処理中状態にする
            _processingMap[dispatchToken] = true;

            // コールバックを呼ぶ
            // このコールバックの中でWaitForが呼ばれる可能性がある
            var callback = _callbacks[dispatchToken];
            callback.Invoke(_dispatchingPayload);

            // 処理完了状態にする
            _processedMap[dispatchToken] = true;
        }
    }
}