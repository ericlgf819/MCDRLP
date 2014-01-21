using System;
using System.ComponentModel;
using System.Configuration.Install;

namespace MCD.RLPlanning.WinService
{
    /// <summary>
    /// 
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        #region ctor

        public ProjectInstaller()
        {
            InitializeComponent();
        }
        #endregion
    }
}