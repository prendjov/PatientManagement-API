using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.API.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data)
        {
            Succeeded = true;
            Mesagge = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Mesagge { get; set; }

    }
}
