using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Models.Requests
{
    public class CheckFileHashesOutput
    {
        public CheckFileHashesOutput(string fileName, bool result, string message)
        {
            FileName = fileName;
            Result = result;
            Message = message;
        }

        public string FileName { get; set; }
        /// <summary>
        /// true - hashes are equal
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// for not equal hashes
        /// </summary>
        public string Message { get; set; }
    }
}
