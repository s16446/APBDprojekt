using System;
using System.Runtime.Serialization;

namespace AdvertApi.Controllers
{
	[Serializable]
	internal class ClientNotFoundException : Exception
	{
		public ClientNotFoundException()
		{
		}

		public ClientNotFoundException(string message) : base(message)
		{
		}

		public ClientNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ClientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}