using System.Runtime.Serialization;

namespace DO
{
    /// <summary>
    /// Object was not found OR does not exist (CRUD - Drop the "C")
    /// </summary>
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException() { }
        public ObjectNotFoundException(string message) : base(message) { }
        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ObjectNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// ID = Object.ID in ADD function (CRUD - Drop the "RUD")
    /// </summary>
    public class DoubleFoundException : Exception
    {
        public DoubleFoundException() { }
        public DoubleFoundException(string message) : base(message) { }
        public DoubleFoundException(string message, Exception inner) : base(message, inner) { }
        protected DoubleFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }

    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
        {
        }

        public AlreadyExistException(string? message) : base(message)
        {
        }

        public AlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}

