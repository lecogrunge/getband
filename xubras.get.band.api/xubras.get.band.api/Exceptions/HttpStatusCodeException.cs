using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace xubras.get.band.api.Exceptions
{
    [Serializable]
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCodeException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, Exception inner) : this(statusCode, inner.ToString())
        {
        }

        public HttpStatusCodeException(int statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            ContentType = @"application/json";
        }

        protected HttpStatusCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("ResourceReferenceProperty", ResourceReferenceProperty);

            base.GetObjectData(info, context);
        }

        public string ResourceReferenceProperty { get; set; }

        public int StatusCode { get; set; }

        public string ContentType { get; set; } = @"text/plain";
    }
}