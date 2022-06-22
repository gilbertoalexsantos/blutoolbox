using Bludk;
using UnityEngine;

namespace BluEngine
{
    public class BuildInfoManager
    {
        private readonly ISerializer _serializer;

        private BuildInfoData _buildInfoData;
        
        public BuildInfoManager(ISerializer serializer)
        {
            _serializer = serializer;
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
            TextAsset buildInfoTextAsset = Resources.Load<TextAsset>("BluEngine/buildInfo");
            _buildInfoData = _serializer.Deserialize<BuildInfoData>(buildInfoTextAsset.text);
        }
    }
}