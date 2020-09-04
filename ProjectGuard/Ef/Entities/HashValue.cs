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

        public string FileName { get; set; }
        public string Hash { get; set; }
    }
}
