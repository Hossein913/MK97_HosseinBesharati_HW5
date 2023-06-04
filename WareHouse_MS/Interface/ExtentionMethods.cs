using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse_MS.Interface
{
    public static class ExtentionMethods
    {
        /// <summary>
        /// helps to Write a text Into a file
        /// </summary>
        /// <param name="FileDirectory">Start file derectory from project location</param>
        public static void WriteOnFile(this string text, string FileDirectory)
        {
            using (StreamWriter writer = new StreamWriter(Common.GetProjectDirectory(FileDirectory)))
            { writer.Write(text); }


        }
    }
}
