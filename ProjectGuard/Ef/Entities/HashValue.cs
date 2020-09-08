namespace ProjectGuard.Ef.Entities
{
    public class HashValue : BaseEntity
    {
        public HashValue(string fileName, string hash)
        {
            FileName = fileName;
            Hash = hash;
        }

        public HashValue()
        {

        }

        /// <summary>
        /// full path
        /// </summary>
        public string FileName { get; set; }
        public string Hash { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
