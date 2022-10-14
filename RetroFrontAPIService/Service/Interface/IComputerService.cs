namespace RetroFrontAPIService.Service.Interface
{
    public interface IComputerService
    {
        bool FileCopy(string sourcefile, string destfile);
        bool FileDownload(string sourcefile, string destfile);
        bool WriteByte(byte[] bytetoWrite, string destfile);
        bool FileMove(string sourcefile, string destfile);
    }
}