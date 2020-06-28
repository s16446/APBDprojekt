using System;
using System.Runtime.Serialization;

namespace Cw11_WebApplication.DAL
{
	[Serializable]
	internal class BuidlingDoesNotExistException : Exception
	{
		public BuidlingDoesNotExistException()
		{
		}

		public BuidlingDoesNotExistException(string message) : base(message)
		{
		}

		public BuidlingDoesNotExistException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected BuidlingDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}