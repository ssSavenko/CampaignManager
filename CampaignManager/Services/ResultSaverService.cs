using Newtonsoft.Json;
using System.Text;

namespace CampaignManager.Services
{
    public interface IResultSaverService
    {
        Task  SaveToFile(string dataToSave);
    }

    public class ResultSaverService : IResultSaverService
    {
        private string resultFile = "sends.json";
        private Object fileWriteSyncObject = new Object();

        public async Task  SaveToFile(string dataToSave)
        {
            lock (fileWriteSyncObject)
            {
                File.WriteAllText(resultFile, dataToSave, Encoding.UTF8);
            }
        }

        public string ResultFile { get => resultFile; set => resultFile = value; }
    }
}
