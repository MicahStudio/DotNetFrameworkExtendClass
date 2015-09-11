namespace DotNetFrameworkExtendClass.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExecuteWithObject
    {
        /// <summary>
        /// 
        /// </summary>
        object Target
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        void ExecuteWithObject(object parameter);
        /// <summary>
        /// 
        /// </summary>
        void MarkForDeletion();
    }
}
