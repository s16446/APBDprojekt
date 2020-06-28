using System;
using System.Runtime.Serialization;

namespace Cw11_WebApplication.DAL
{
	[Serializable]
	internal class ClientAlreadyExistException : Exception
	{
		public ClientAlreadyExistException()
		{
		}

		public ClientAlreadyExistException(string message) : base(message)
		{
		}

		public ClientAlreadyExistException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ClientAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}