using Bludk;
using UnityEngine;

namespace BluEngine
{
    public class GameSettingsDatasource : ISyncDatasource<GameSettings>
    {
        public GameSettings LoadSync()
        {
            return Resources.Load<GameSettings>(GameSettings.ResourcesPath);
        }
    }
}