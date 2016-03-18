using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DotNetFrameworkExtendClass.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class Messenger : IMessenger
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly object CreationLock = new object();
        /// <summary>
        /// 
        /// </summary>
        private static IMessenger _defaultInstance;
        /// <summary>
        /// 
        /// </summary>
        private readonly object _registerLock = new object();
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Type, List<WeakActionAndToken>> _recipientsOfSubclassesAction;
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Type, List<WeakActionAndToken>> _recipientsStrictAction;
        /// <summary>
        /// 默认实例
        /// </summary>
        public static IMessenger Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        if (_defaultInstance == null)
                        {
                            _defaultInstance = new Messenger();
                        }
                    }
                }
                return _defaultInstance;
            }
        }

        #region IMessenger Members
        /// <summary>
        /// 注册一个新的消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="action">执行委托的方法</param>
        public virtual void Register<TMessage>(object recipient, Action<TMessage> action)
        {
            Register(recipient, null, false, action);
        }
        /// <summary>
        /// 注册一个新的消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="receiveDerivedMessagesToo"></param>
        /// <param name="action">执行委托的方法</param>
        public virtual void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action)
        {
            Register(recipient, null, receiveDerivedMessagesToo, action);
        }
        /// <summary>
        /// 注册一个新的消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="token">标识符</param>
        /// <param name="action">执行委托的方法</param>
        public virtual void Register<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            Register(recipient, token, false, action);
        }
        /// <summary>
        /// 订阅一个消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="token">标识符</param>
        /// <param name="receiveDerivedMessagesToo"></param>
        /// <param name="action">执行委托的方法</param>
        public virtual void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action)
        {
            lock (_registerLock)
            {
                var messageType = typeof(TMessage);
                Dictionary<Type, List<WeakActionAndToken>> recipients;
                if (receiveDerivedMessagesToo)
                {
                    if (_recipientsOfSubclassesAction == null)
                    {
                        _recipientsOfSubclassesAction = new Dictionary<Type, List<WeakActionAndToken>>();
                    }
                    recipients = _recipientsOfSubclassesAction;
                }
                else
                {
                    if (_recipientsStrictAction == null)
                    {
                        _recipientsStrictAction = new Dictionary<Type, List<WeakActionAndToken>>();
                    }
                    recipients = _recipientsStrictAction;
                }

                lock (recipients)
                {
                    List<WeakActionAndToken> list;
                    if (!recipients.ContainsKey(messageType))
                    {
                        list = new List<WeakActionAndToken>();
                        recipients.Add(messageType, list);
                    }
                    else
                    {
                        list = recipients[messageType];
                    }
                    var weakAction = new WeakAction<TMessage>(recipient, action);
                    var item = new WeakActionAndToken
                    {
                        Action = weakAction,
                        Token = token
                    };

                    list.Add(item);
                }
            }
            RequestCleanup();
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _isCleanupRegistered;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        public virtual void Send<TMessage>(TMessage message)
        {
            SendToTargetOrType(message, null, null);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="message"></param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Send<TMessage, TTarget>(TMessage message)
        {
            SendToTargetOrType(message, typeof(TTarget), null);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="token"></param>
        public virtual void Send<TMessage>(TMessage message, object token)
        {
            SendToTargetOrType(message, null, token);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="recipient">受体,一般都用this</param>
        public virtual void Unregister(object recipient)
        {
            UnregisterFromLists(recipient, _recipientsOfSubclassesAction);
            UnregisterFromLists(recipient, _recipientsStrictAction);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Unregister<TMessage>(object recipient)
        {
            Unregister<TMessage>(recipient, null, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="token"></param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        public virtual void Unregister<TMessage>(object recipient, object token)
        {
            Unregister<TMessage>(recipient, token, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="action"></param>
        public virtual void Unregister<TMessage>(object recipient, Action<TMessage> action)
        {
            Unregister(recipient, null, action);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient">受体,一般都用this</param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        public virtual void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            UnregisterFromLists(recipient, token, action, _recipientsStrictAction);
            UnregisterFromLists(recipient, token, action, _recipientsOfSubclassesAction);
            RequestCleanup();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newMessenger"></param>
        public static void OverrideDefault(IMessenger newMessenger)
        {
            _defaultInstance = newMessenger;
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Reset()
        {
            _defaultInstance = null;
        }
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Non static access is needed.")]
        public void ResetAll()
        {
            Reset();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lists"></param>
        private static void CleanupList(IDictionary<Type, List<WeakActionAndToken>> lists)
        {
            if (lists == null)
            {
                return;
            }
            lock (lists)
            {
                var listsToRemove = new List<Type>();
                foreach (var list in lists)
                {
                    var recipientsToRemove = list.Value.Where(item => item.Action == null || !item.Action.IsAlive).ToList();
                    foreach (var recipient in recipientsToRemove)
                    {
                        list.Value.Remove(recipient);
                    }
                    if (list.Value.Count == 0)
                    {
                        listsToRemove.Add(list.Key);
                    }
                }
                foreach (var key in listsToRemove)
                {
                    lists.Remove(key);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="weakActionsAndTokens"></param>
        /// <param name="messageTargetType"></param>
        /// <param name="token"></param>
        private static void SendToList<TMessage>(TMessage message, IEnumerable<WeakActionAndToken> weakActionsAndTokens, Type messageTargetType, object token)
        {
            if (weakActionsAndTokens != null)
            {
                var list = weakActionsAndTokens.ToList();
                var listClone = list.Take(list.Count()).ToList();
                foreach (var item in listClone)
                {
                    var executeAction = item.Action as IExecuteWithObject;
                    if (executeAction != null && item.Action.IsAlive && item.Action.Target != null && (messageTargetType == null || item.Action.Target.GetType() == messageTargetType || messageTargetType.IsAssignableFrom(item.Action.Target.GetType())) && ((item.Token == null && token == null) || item.Token != null && item.Token.Equals(token)))
                    {
                        executeAction.ExecuteWithObject(message);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="lists"></param>
        private static void UnregisterFromLists(object recipient, Dictionary<Type, List<WeakActionAndToken>> lists)
        {
            if (recipient == null || lists == null || lists.Count == 0)
            {
                return;
            }
            lock (lists)
            {
                foreach (var messageType in lists.Keys)
                {
                    foreach (var item in lists[messageType])
                    {
                        var weakAction = (IExecuteWithObject)item.Action;
                        if (weakAction != null && recipient == weakAction.Target)
                        {
                            weakAction.MarkForDeletion();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        /// <param name="lists"></param>
        private static void UnregisterFromLists<TMessage>(object recipient, object token, Action<TMessage> action, Dictionary<Type, List<WeakActionAndToken>> lists)
        {
            var messageType = typeof(TMessage);
            if (recipient == null || lists == null || lists.Count == 0 || !lists.ContainsKey(messageType))
            {
                return;
            }
            lock (lists)
            {
                foreach (var item in lists[messageType])
                {
                    var weakActionCasted = item.Action as WeakAction<TMessage>;

                    if (weakActionCasted != null && recipient == weakActionCasted.Target && (action == null || action.Method.Name == weakActionCasted.MethodName) && (token == null || token.Equals(item.Token)))
                    {
                        item.Action.MarkForDeletion();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void RequestCleanup()
        {
            if (!_isCleanupRegistered)
            {
                Action cleanupAction = Cleanup;
                _isCleanupRegistered = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Cleanup()
        {
            CleanupList(_recipientsOfSubclassesAction);
            CleanupList(_recipientsStrictAction);
            _isCleanupRegistered = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="messageTargetType"></param>
        /// <param name="token"></param>
        private void SendToTargetOrType<TMessage>(TMessage message, Type messageTargetType, object token)
        {
            var messageType = typeof(TMessage);
            if (_recipientsOfSubclassesAction != null)
            {
                var listClone = _recipientsOfSubclassesAction.Keys.Take(_recipientsOfSubclassesAction.Count()).ToList();
                foreach (var type in listClone)
                {
                    List<WeakActionAndToken> list = null;
                    if (messageType == type || messageType.IsSubclassOf(type) || type.IsAssignableFrom(messageType))
                    {
                        lock (_recipientsOfSubclassesAction)
                        {
                            list = _recipientsOfSubclassesAction[type].Take(_recipientsOfSubclassesAction[type].Count()).ToList();
                        }
                    }
                    SendToList(message, list, messageTargetType, token);
                }
            }
            if (_recipientsStrictAction != null)
            {
                List<WeakActionAndToken> list = null;
                lock (_recipientsStrictAction)
                {
                    if (_recipientsStrictAction.ContainsKey(messageType))
                    {
                        list = _recipientsStrictAction[messageType].Take(_recipientsStrictAction[messageType].Count()).ToList();
                    }
                }
                if (list != null)
                {
                    SendToList(message, list, messageTargetType, token);
                }
            }
            RequestCleanup();
        }
        #region Nested type: WeakActionAndToken
        /// <summary>
        /// 
        /// </summary>
        private struct WeakActionAndToken
        {
            public WeakAction Action;
            public object Token;
        }
        #endregion
    }
}
