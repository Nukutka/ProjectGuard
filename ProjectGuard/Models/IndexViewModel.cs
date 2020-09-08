using ProjectGuard.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
