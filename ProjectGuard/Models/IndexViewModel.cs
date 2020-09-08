using ProjectGuard.Ef.Entities;
using System.Collections.Generic;

namespace ProjectGuard.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(List<Project> projects)
        {
            Projects = projects;
        }

        public IndexViewModel()
        {

        }

        public List<Project> Projects { get; set; }
    }
}
