using System.ComponentModel;

namespace SocialNetwork.Exceptions
{
    public class BaseException : Exception
    {
        public ResponseCode Code { get; set; }

        public BaseException(ResponseCode code) : base("")
        {
            Code = code;
        }

        public BaseException(ResponseCode code, string message) : base(message)
        {
            Code = code;
        }

        public BaseException(ResponseCode code, Exception inner) : base("", inner)
        {
            Code = code;
        }
    }
}