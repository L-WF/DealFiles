using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DealFiles
{
    class Program
    {
        static DirectoryInfo begin = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\begin");
        static DirectoryInfo end = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\end");
        static DirectoryInfo[] dirs = begin.GetDirectories();
        static List<string> beginFileNames = new List<string>();
        static List<string> repeatFileNames = new List<string>();


        static void Main(string[] args)
        {
            Console.WriteLine("                              林伟丰——18826418792");
            Console.WriteLine("进行中……");
            GetBeginNames();
            GetRepeatNames();
            CreateSubDirs();
            MoveFiles();
            Console.WriteLine("已完成，按任意键退出……");
            Console.ReadKey();
        }

        static void GetBeginNames()
        {
            foreach (var dir in dirs)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (var file in files)
                {
                    beginFileNames.Add(file.Name);
                }
            }
        }

        static void GetRepeatNames()
        {
            int count = beginFileNames.Count;
            for( int i=0 ; i<count ; i++ )
            {
                for (int j = i+1; j<count ; j++)
                {
                    if( string.Compare(beginFileNames[i],beginFileNames[j]) == 0 )
                    {
                        AddRepeatNames(beginFileNames[i]);
                        break;
                    }
                }
            }
        }

        static void AddRepeatNames(string s)
        {
            foreach (var repeatFileName in repeatFileNames)
            {
                if ( string.Compare(repeatFileName,s) == 0 )
                {
                    return;
                }
            }
            repeatFileNames.Add(s);
        }

        static void CreateSubDirs()
        {
            foreach (var repeatFileName in repeatFileNames)
            {
                int length = repeatFileName.IndexOf(".");
                end.CreateSubdirectory(repeatFileName.Substring(0, length));
            }
        }

        static void MoveFiles()
        {
            foreach (var dir in dirs)
            {
                FileInfo[] Files = dir.GetFiles();
                foreach (var File in Files)
                {
                    foreach (var repeatFileName in repeatFileNames)
                    {
                        if( string.Compare(File.Name,repeatFileName) == 0 )
                        {
                            int length = repeatFileName.IndexOf(".");
                            DirectoryInfo targetDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\end\\" + repeatFileName.Substring(0, length));
                            string copyName = targetDir.GetFiles().Count() + File.Extension;
                            System.IO.File.Copy(File.FullName,targetDir.FullName+"\\"+copyName);
                            break;
                        }
                    }
                }
            }
        }
    }
}
