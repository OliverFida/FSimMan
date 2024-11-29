using OF.Base.Client;

namespace OF.FSimMan.Client.ImportExport
{
    public interface IImportExportClient<T> : IClient
    {
        public void Export(string targetFilePath, T objectToExport);
        public void Import(string sourceFilePath);
    }
}