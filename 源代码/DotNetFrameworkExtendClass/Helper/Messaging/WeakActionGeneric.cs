using System;
using System.Diagnostics.CodeAnalysis;

namespace DotNetFrameworkExtendClass.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        /// <summary>
        /// 
        /// </summary>
        private Action<T> _staticAction;
        /// <summary>
        /// 
        /// </summary>
        public override string MethodName
        {
            get
            {
                if (_staticAction != null)
                {
                    return _staticAction.Method.Name;
                }

                return Method.Name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool IsAlive
        {
            get
            {
                if (_staticAction == null && Reference == null)
                {
                    return false;
                }

                if (_staticAction != null)
                {
                    if (Reference != null)
                    {
                        return Reference.IsAlive;
                    }

                    return true;
                }
                return Reference.IsAlive;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public WeakAction(Action<T> action) : this(action == null ? null : action.Target, action)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="action"></param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Method should fail with an exception if action is null.")]
        public WeakAction(object target, Action<T> action)
        {
            if (action.Method.IsStatic)
            {
                _staticAction = action;

                if (target != null)
                {
                    Reference = new WeakReference(target);
                }

                return;
            }

            Method = action.Method;
            ActionReference = new WeakReference(action.Target);

            Reference = new WeakReference(target);
        }
        /// <summary>
        /// 
        /// </summary>
        public new void Execute()
        {
            Execute(default(T));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(T parameter)
        {
            if (_staticAction != null)
            {
                _staticAction(parameter);
                return;
            }

            var actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null && ActionReference != null && actionTarget != null)
                {
                    Method.Invoke(actionTarget, new object[] { parameter });
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void ExecuteWithObject(object parameter)
        {
            var parameterCasted = (T)parameter;
            Execute(parameterCasted);
        }
        /// <summary>
        /// 
        /// </summary>
        public new void MarkForDeletion()
        {
            _staticAction = null;
            base.MarkForDeletion();
        }
    }
}
