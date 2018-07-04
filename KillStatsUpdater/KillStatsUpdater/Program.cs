using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillStatsUpdater
{
    class Program
    {
        private static string Path = Application.StartupPath;
        static void Main(string[] args)
        {
            try
            {
                Directory.CreateDirectory(Program.Path + "\\KillStatsExtrac");
                Console.WriteLine("New directory \"" + Program.Path + "\\KillStatsExtrac \" created");
                ZipFile.ExtractToDirectory(Program.Path + "\\KillStats.zip", Program.Path + "\\KillStatsExtrac");
                Console.WriteLine(string.Concat(new string[]
                {
                    "Extracted ",
                    Program.Path,
                    "\\KillStats.zip  to  ",
                    Program.Path,
                    "\\KillStatsExtrac"
                }));
                DirectoryInfo directoryInfo = new DirectoryInfo(Program.Path + "\\KillStatsExtrac");
                Console.WriteLine("Updating KillStats.exe");
                File.Replace(Program.Path + "\\KillStatsExtrac\\KillStats-Bin\\KillStats.exe", Directory.GetParent(Program.Path).FullName + "\\KillStats.exe", Directory.GetParent(Program.Path).FullName + "\\KillStats_old.exe");
                Console.WriteLine("Updating version.json");
                File.Replace(Program.Path + "\\KillStatsExtrac\\KillStats-Bin\\update\\version.json", Program.Path + "\\version.json", Program.Path + "\\version_old.json");
                Console.WriteLine("Updating Resources");
                for (int i = 0; i <= 5; i++)
                {
                    try
                    {
                        DirectoryInfo directoryInfo2 = new DirectoryInfo(Directory.GetParent(Program.Path).FullName + "\\resources");
                        DirectoryInfo[] directories = directoryInfo2.GetDirectories();
                        for (int j = 0; j < directories.Length; j++)
                        {
                            DirectoryInfo directoryInfo3 = directories[j];
                            directoryInfo3.Delete(true);
                        }
                        FileInfo[] files = directoryInfo2.GetFiles();
                        for (int k = 0; k < files.Length; k++)
                        {
                            FileInfo fileInfo = files[k];
                            fileInfo.Delete();
                        }
                        Directory.Delete(Directory.GetParent(Program.Path).FullName + "\\resources", true);
                    }
                    catch
                    {
                        Console.WriteLine("Recources folder couldn't be deleted. Try: " + (i + 1));
                    }
                    try
                    {
                        Directory.Move(Program.Path + "\\KillStatsExtrac\\KillStats-Bin\\resources", Directory.GetParent(Program.Path).FullName + "\\resources");
                        i = 6;
                    }
                    catch
                    {
                        Thread.Sleep(3000);
                    }
                }
                Program.CheckVersion_0_3_0();
                Program.CheckVersion_0_4_0();
                Program.CheckVersion_0_5_0();
                DirectoryInfo[] directories2 = directoryInfo.GetDirectories();
                for (int l = 0; l < directories2.Length; l++)
                {
                    DirectoryInfo directoryInfo4 = directories2[l];
                    directoryInfo4.Delete(true);
                }
                Directory.Delete(Program.Path + "\\KillStatsExtrac");
                File.Delete(Program.Path + "\\KillStats.zip");
                Console.WriteLine("Removed directory " + Program.Path + "\\KillStatsExtrac");
            }
            catch (Exception ex)
            {
                DirectoryInfo directoryInfo5 = new DirectoryInfo(Program.Path + "\\KillStatsExtrac");
                DirectoryInfo[] directories3 = directoryInfo5.GetDirectories();
                for (int m = 0; m < directories3.Length; m++)
                {
                    DirectoryInfo directoryInfo6 = directories3[m];
                    directoryInfo6.Delete(true);
                }
                Directory.Delete(Program.Path + "\\KillStatsExtrac");
                File.Delete(Program.Path + "\\KillStats.zip");
                File.WriteAllText(Program.Path + string.Format("\\UpdateFailed{0}.log", DateTime.Now.ToBinary().ToString()), "----------Update Failed----------\n\n" + ex.ToString());
                Console.WriteLine("\n----------Update Failed----------\n\n" + ex.ToString());
                Console.Read();
            }
            try
            {
                Process.Start(Directory.GetParent(Program.Path).FullName + "\\KillStats.exe");
            }
            catch
            {
                Console.WriteLine("Failed to start KillStats.exe");
            }
            Process.Start(new ProcessStartInfo
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

        private static void CheckVersion_0_3_0()
        {
            bool flag = !Directory.Exists(Directory.GetParent(Program.Path).FullName + "\\config\\presets");
            if (flag)
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetParent(Program.Path).FullName + "\\config");
                    DirectoryInfo[] directories = directoryInfo.GetDirectories();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        DirectoryInfo directoryInfo2 = directories[i];
                        directoryInfo2.Delete(true);
                    }
                    FileInfo[] files = directoryInfo.GetFiles();
                    for (int j = 0; j < files.Length; j++)
                    {
                        FileInfo fileInfo = files[j];
                        fileInfo.Delete();
                    }
                    Directory.Delete(Directory.GetParent(Program.Path).FullName + "\\config", true);
                    Directory.Move(Program.Path + "\\KillStatsExtrac\\KillStats-Bin\\config", Directory.GetParent(Program.Path).FullName + "\\config");
                }
                catch
                {
                    Console.WriteLine("Failed to install Version 0.3.0");
                }
            }
            else
            {
                Console.WriteLine("Version 0.3.0 already installed");
            }
        }

        private static void CheckVersion_0_4_0()
        {
            bool flag = !Directory.Exists(Directory.GetParent(Program.Path).FullName + "\\saves");
            if (flag)
            {
                Directory.CreateDirectory(Directory.GetParent(Program.Path).FullName + "\\saves");
            }
        }

        private static void CheckVersion_0_5_0()
        {
            bool flag = !Directory.Exists(Directory.GetParent(Program.Path).FullName + "\\config\\autorun");
            if (flag)
            {
                Directory.Move(Program.Path + "\\KillStatsExtrac\\KillStats-Bin\\config\\autorun", Directory.GetParent(Program.Path).FullName + "\\config\\autorun");
            }
        }
    }
}
