using System.Collections.Generic;

namespace ProjectGuard.Ef.Entities
{
    public class Project : BaseEntity
    {
        public Project(string name, string path, ICollection<HashValue> hashValues)
        {
            Name = name;
            Path = path;
            HashValues = hashValues;
        }

        public Project()
        {

        }

        public string Name { get; set; }
        public string Path { get; set; }
        public ICollection<HashValue> HashValues { get; set; }
    }
}
