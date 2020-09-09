using System.Collections.Generic;

namespace ProjectGuard.Ef.Entities
{
    public class Project : BaseEntity
    {
        public Project(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public Project()
        {

        }

        public string Name { get; set; }
        public string Path { get; set; }
        public ICollection<HashValue> HashValues { get; set; }
    }
}
