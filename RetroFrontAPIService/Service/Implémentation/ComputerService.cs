using RetroFrontAPIService.Service.Interface;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service.Implémentation
{
    public class ComputerService : IComputerService
    {
        public bool FileCopy(string sourcefile, string destfile)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destfile));
                //System.IO.File.Create(destfile);
                System.IO.File.Copy(sourcefile, destfile, true);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool FileMove(string sourcefile, string destfile)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destfile));
                //System.IO.File.Create(destfile);
                System.IO.File.Move(sourcefile, destfile, true);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        public bool FileDownload(string sourcefile, string destfile)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destfile));
                    //System.IO.File.Create(destfile);
                    var t = Task.Run(() => wc.DownloadFile(new Uri(sourcefile), destfile));
                    //wc.DownloadFileAsync(new Uri(sourcefile), destfile);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool WriteByte(byte[] bytetoWrite, string destfile)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destfile));
                //System.IO.File.Create(destfile);
                var t = Task.Run(() => File.WriteAllBytes(destfile, bytetoWrite));
                //await File.WriteAllBytesAsync(destfile, bytetoWrite);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
