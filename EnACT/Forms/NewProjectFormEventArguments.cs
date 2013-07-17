using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnACT
{
    /// <summary>
    /// Provides data for the ProjectCreated Event.
    /// </summary>
    public class ProjectCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The project created by the new form.
        /// </summary>
        public ProjectInfo ProjectInfo { private set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCreatedEventArgs" /> class.
        /// </summary>
        /// <param name="projectInfo">The ProjectInfo class that was created.</param>
        public ProjectCreatedEventArgs(ProjectInfo projectInfo)
        {
            this.ProjectInfo = projectInfo;
        }
    }
}
