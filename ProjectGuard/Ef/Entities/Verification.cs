using System.Collections.Generic;

namespace ProjectGuard.Ef.Entities
{
    public class Verification : BaseEntity
    {
        public Verification(bool result)
        {
            Result = result;
            FileCheckResults = new List<FileCheckResult>();
        }

        public Verification()
        {
            FileCheckResults = new List<FileCheckResult>();
        }

        /// <summary>
        /// true - прошло, false - нет
        /// </summary>
        public bool Result { get; set; }
        public ICollection<FileCheckResult> FileCheckResults { get; set; }
    }
}
