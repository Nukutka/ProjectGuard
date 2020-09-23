using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Models
{
    public class ChangeFilesNeedHashInput
    {
        public List<FileNeedHash> NeedHashes { get; set; }
    }

    public class FileNeedHash
    {
        public int FileId { get; set; }
        public bool NeedHash { get; set; }
    }
}
