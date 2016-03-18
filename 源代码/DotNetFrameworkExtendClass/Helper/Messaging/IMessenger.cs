using System;
using System.Diagnostics.CodeAnalysis;

namespace DotNetFrameworkExtendClass.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="action"></param>
        void Register<TMessage>(object recipient, Action<TMessage> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        void Register<TMessage>(object recipient, object token, Action<TMessage> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        /// <param name="receiveDerivedMessagesToo"></param>
        /// <param name="action"></param>
        void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="receiveDerivedMessagesToo"></param>
        /// <param name="action"></param>
        void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        void Send<TMessage>(TMessage message);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="message"></param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        void Send<TMessage, TTarget>(TMessage message);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="token"></param>
        void Send<TMessage>(TMessage message, object token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipient"></param>
        void Unregister(object recipient);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        void Unregister<TMessage>(object recipient);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This syntax is more convenient than other alternatives.")]
        void Unregister<TMessage>(object recipient, object token);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="action"></param>
        void Unregister<TMessage>(object recipient, Action<TMessage> action);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        void Unregister<TMessage>(object recipient, object token, Action<TMessage> action);
    }
}
