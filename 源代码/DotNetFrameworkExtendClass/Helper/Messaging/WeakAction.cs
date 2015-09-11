using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DotNetFrameworkExtendClass.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public class WeakAction
    {
        /// <summary>
        /// 
        /// </summary>
        private Action _staticAction;
        /// <summary>
        /// 
        /// </summary>
        protected MethodInfo Method
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string MethodName
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
        protected WeakReference ActionReference
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        protected WeakReference Reference
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return _staticAction != null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected WeakAction()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public WeakAction(Action action) : this(action == null ? null : action.Target, action)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="action"></param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Method should fail with an exception if action is null.")]
        public WeakAction(object target, Action action)
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
        public virtual bool IsAlive
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
        public object Target
        {
            get
            {
                if (Reference == null)
                {
                    return null;
                }

                return Reference.Target;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected object ActionTarget
        {
            get
            {
                if (ActionReference == null)
                {
                    return null;
                }
                return ActionReference.Target;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            if (_staticAction != null)
            {
                _staticAction();
                return;
            }
            var actionTarget = ActionTarget;
            if (IsAlive)
            {
                if (Method != null && ActionReference != null && actionTarget != null)
                {
                    Method.Invoke(actionTarget, null);
                    return;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            Method = null;
            _staticAction = null;
        }
    }
}
