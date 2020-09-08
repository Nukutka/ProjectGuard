namespace ProjectGuard.Ef.Entities
{
    public class HashValue : BaseEntity
    {
        public HashValue(string fileName)
        {
            FileName = fileName;
            NeedHash = false;
        }

        public HashValue(string fileName, bool needHash, string hash)
        {
            FileName = fileName;
            NeedHash = needHash;
            Hash = hash;
        }

        public HashValue()
        {

        }

        /// <summary>
        /// full path
        /// </summary>
        public string FileName { get; set; }
        public bool NeedHash { get; set; }
        public string Hash { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
