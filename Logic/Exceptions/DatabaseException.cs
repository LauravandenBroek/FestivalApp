
namespace Logic.Exceptions
{
    public class TemporaryDatabaseException : Exception 
    {
        private const string DefaultMessage = "There is a temporary problem, please try again later";
        public TemporaryDatabaseException() : base(DefaultMessage) { }
    }

    public class PersistentDatabaseException : Exception
    {
        private const string DefaultMessage = "There is a temporary problem, please try again later";

        public PersistentDatabaseException() : base(DefaultMessage) { }
    }
}
