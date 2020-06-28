using System;
using System.Runtime.Serialization;

namespace Cw11_WebApplication.DAL
{
	[Serializable]
	internal class StreetDoesNotMatchException : Exception
	{
		public StreetDoesNotMatchException()
		{
		}

		public StreetDoesNotMatchException(string message) : base(message)
		{
		}

		public StreetDoesNotMatchException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected StreetDoesNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}