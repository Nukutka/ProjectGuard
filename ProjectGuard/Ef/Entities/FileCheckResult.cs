namespace ProjectGuard.Ef.Entities
{
    public class FileCheckResult : BaseEntity
    {
        public FileCheckResult(int hashValueId, int verificationId)
        {
            HashValueId = hashValueId;
            VerificationId = verificationId;
        }

        public FileCheckResult(int hashValueId)
        {
            HashValueId = hashValueId;
        }

        public FileCheckResult()
        {

        }

        public HashValue HashValue { get; set; }
        public int HashValueId { get; set; }
        /// <summary>
        /// true - прошло, false - нет
        /// </summary>
        public bool Result { get; set; }
        public string Message { get; set; }
        public Verification Verification { get; set; }
        public int VerificationId { get; set; }
    }
}
