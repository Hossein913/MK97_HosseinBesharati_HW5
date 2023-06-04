using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse_MS
{
    public static class Common
    {
        public static string GetProjectDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            return projectDirectory;
        }

        public static string GetProjectDirectory(string Filename)
        {
            string FullDirectory = String.Concat(GetProjectDirectory(), Filename);
            return FullDirectory;
        }
    }
}
