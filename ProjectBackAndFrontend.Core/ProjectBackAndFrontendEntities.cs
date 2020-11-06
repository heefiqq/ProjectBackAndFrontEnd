using System.Data.Entity;

namespace ProjectBackAndFrontend.Core
{
    class ProjectBackAndFrontendEntities : DbContext
    {

        public ProjectBackAndFrontendEntities() : base("name=ProjectBackAndFrontendEntities")
        {
        }
    }
}