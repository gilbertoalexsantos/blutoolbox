using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Bludk
{
    public class UnityDeviceIdProvider : IDeviceIdProvider
    {
        public string DeviceId
        {
            get
            {
                string deviceId = SystemInfo.deviceUniqueIdentifier;
                using var md5Hash = MD5.Create();
                byte[] sourceBytes = Encoding.UTF8.GetBytes(deviceId);
                byte[] hashBytes = md5Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash;
            }
        }
    }
}