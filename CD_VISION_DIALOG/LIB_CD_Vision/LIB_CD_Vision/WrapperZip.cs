using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Ionic.Zip;
using CD_Measure;

namespace CD_Measure
{
    public static class WrapperZip
    {
        public static bool ExtractDirect(string strZipFilePath, string strTargetFileName, string strDestination)
        {
            bool bSucess = true;

            try
            {
                using (ZipFile zip = ZipFile.Read(strZipFilePath))
                {
                    ZipEntry e = zip[strTargetFileName];
                    e.Extract(strDestination, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                bSucess = false;
            }

            return bSucess;
            
        }
        public static List<string> GetFileList_Without_unzip(string strPathZipFile)
        {
            List<String> list = new List<string>();

            using (ZipFile zip = ZipFile.Read(strPathZipFile))
            {
                foreach (ZipEntry e in zip)
                {
                    //string strFileName = Path.GetFileName(e.FileName);
                    list.Add(e.FileName);
                }
            }
            return list;
        }

        public static Boolean UpZip(string strPathZipFile, string strDestination)
        {
            bool bSucess = true;

            using (ZipFile zip = ZipFile.Read(strPathZipFile))
            {
                try
                {
                    if (File.Exists(strPathZipFile) == true)
                    {
                        if (Directory.Exists(strDestination) == false)
                        {
                            Computer.CreateFolder(strDestination);
                        }
                        zip.ExtractAll(strDestination);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    bSucess = false;
                }
            }
            return bSucess;
        }
        public static Boolean CreateZipFiles(List<string> listFiles, string strZipFilePath)
        {
            Boolean bSuccess = true;
            
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    for (int i = 0; i < listFiles.Count; i++)
                    {
                        string strFilePath = listFiles.ElementAt(i);

                        if (Computer.isValidPath(strFilePath) == true)
                        {
                            zip.AddFile(strFilePath);
                        }
                    }
                    zip.Save(strZipFilePath);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                bSuccess = false;
            }
            return bSuccess;
        }

        public static Boolean CreateZipDirectory(string strDirectory, string strZipFilePath)
        {
            Boolean bSuccess = true;

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    string [] files = Computer.GetFileList_within_subdir(strDirectory);

                    for (int i = 0; i < files.Length; i++)
                    {
                        string strFilePath = files[i].ToUpper();

                        if (Computer.isValidPath(strFilePath) == true)
                        {
                            zip.AddFile( strFilePath);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                bSuccess = false;
            }

            return bSuccess;

        }
    }
}
