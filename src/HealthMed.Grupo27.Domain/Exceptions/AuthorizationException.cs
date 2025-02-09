using System.Runtime.Serialization;

namespace HealthMed.Grupo27.Domain.Exceptions
{
    [Serializable]
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base("Acesso não autorizado.") { }

        public AuthorizationException(string message) : base(message) { }

        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
