namespace sheego.Framework.Data.Shared
{
    public interface IConfigurationService
    {
        string GetObjectFilename(string objectType, string id, string extension);
        object GetFilesPath(string objectType);
    }
}
