using System.Collections.Generic;

namespace ProjectGuard.Ef.Entities
{
    public class Verification : BaseEntity
    {
        public Verification(bool result, int projectId)
        {
            Result = result;
            ProjectId = projectId;
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
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
