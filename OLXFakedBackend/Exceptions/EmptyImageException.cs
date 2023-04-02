using System;
namespace OLXFakedBackend.Exceptions
{
	public class EmptyImageException: Exception
	{
		public EmptyImageException()
		{
		}

        public EmptyImageException(string message): base(message)
        {
        }
    }
}

