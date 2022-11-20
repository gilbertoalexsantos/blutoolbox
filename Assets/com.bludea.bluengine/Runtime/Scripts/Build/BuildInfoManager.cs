using Bludk;
using UnityEngine;

namespace BluEngine
{
    public class BuildInfoManager
    {
        private readonly ISerializer _serializer;
        private readonly ISyncDatasource<GameSettings> _gameSettingsDatasource;

        private BuildInfoData _buildInfoData;

        public BuildInfoManager(
            ISerializer serializer,
            ISyncDatasource<GameSettings> gameSettingsDatasource
        )
        {
            _serializer = serializer;
            _gameSettingsDatasource = gameSettingsDatasource;
        }

        public BuildInfoData BuildInfoData
        {
            get
            {
                if (_buildInfoData == null)
                {
                    LoadBuildInfoData();
                }

                return _buildInfoData;
            }
        }

        private void LoadBuildInfoData()
        {
            GameSettings gameSettings = _gameSettingsDatasource.LoadSync();
            TextAsset buildInfoTextAsset = Resources.Load<TextAsset>(gameSettings.BuildInfoResourcesPath);
            _buildInfoData = _serializer.Deserialize<BuildInfoData>(buildInfoTextAsset.text);
        }
    }
}