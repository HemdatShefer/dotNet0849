using System.Runtime.Serialization;

namespace DalXml.xml
{
    [Serializable]
    internal class LoadingException : Exception
    {
        private string v1;
        private string v2;
        private Exception ex;

        public LoadingException()
        {
        }

        public LoadingException(string? message) : base(message)
        {
        }

        public LoadingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public LoadingException(string v1, string v2, Exception ex)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.ex = ex;
        }

        protected LoadingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}