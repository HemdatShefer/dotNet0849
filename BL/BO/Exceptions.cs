using System.Runtime.Serialization;

namespace BlImplementation
{
    [Serializable]
    public class unValidException : Exception
    {
        public unValidException() { }
        public unValidException(string? message) : base(message) { }
        public unValidException(string? message, Exception? innerException) : base(message, innerException) { }
        protected unValidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class NotInStockException : Exception
    {
        public NotInStockException() { }
        public NotInStockException(string? message) : base(message) { }
        public NotInStockException(string? message, Exception? innerException) : base(message, innerException) { }
        protected NotInStockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class IDNotAdmissibleException : Exception
    {
        public IDNotAdmissibleException() { }
        public IDNotAdmissibleException(string? message) : base(message) { }
        public IDNotAdmissibleException(string? message, Exception? innerException) : base(message, innerException) { }
        protected IDNotAdmissibleException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class StockNotAdmissibleException : Exception
    {
        public StockNotAdmissibleException()
        {
        }

        public StockNotAdmissibleException(string? message) : base(message)
        {
        }

        public StockNotAdmissibleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public StockNotAdmissibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class PriceNotAdmissibleException : Exception
    {
        public PriceNotAdmissibleException()
        {
        }

        public PriceNotAdmissibleException(string? message) : base(message)
        {
        }

        public PriceNotAdmissibleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PriceNotAdmissibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class NameNotAdmissibleException : Exception
    {
        public NameNotAdmissibleException()
        {
        }

        public NameNotAdmissibleException(string? message) : base(message)
        {
        }

        public NameNotAdmissibleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NameNotAdmissibleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException() { }
        public ObjectNotFoundException(string? message) : base(message) { }
        public ObjectNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class unValidProductException : Exception
    {
        public unValidProductException()
        {
        }

        public unValidProductException(string? message) : base(message)
        {
        }

        public unValidProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected unValidProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    [Serializable]
    public class TotalUnvalidException : Exception
    {
        public TotalUnvalidException()
        {
        }

        public TotalUnvalidException(string? message) : base(message)
        {
        }

        public TotalUnvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TotalUnvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class AdressUnvalidException : Exception
    {
        public AdressUnvalidException()
        {
        }

        public AdressUnvalidException(string? message) : base(message)
        {
        }

        public AdressUnvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AdressUnvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class NameUnvalidException : Exception
    {
        public NameUnvalidException()
        {
        }

        public NameUnvalidException(string? message) : base(message)
        {
        }

        public NameUnvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NameUnvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class EmailUnvalidException : Exception
    {
        public EmailUnvalidException()
        {
        }

        public EmailUnvalidException(string? message) : base(message)
        {
        }

        public EmailUnvalidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailUnvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class EmptyCartException : Exception
    {
        public EmptyCartException()
        {
        }

        public EmptyCartException(string? message) : base(message)
        {
        }

        public EmptyCartException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyCartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}