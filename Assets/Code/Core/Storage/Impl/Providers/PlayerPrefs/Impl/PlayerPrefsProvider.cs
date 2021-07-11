using AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Interface;

namespace AssemblyCSharp.Assets.Code.Core.Storage.Impl.Providers.PlayerPrefs.Impl
{
    public class PlayerPrefsProvider : IPlayerPrefsProvider
    {
        public bool HasKey(string key)
        {
            return UnityEngine.PlayerPrefs.HasKey(key);
        }

        public string GetString(string key)
        {
            return UnityEngine.PlayerPrefs.GetString(key);
        }

        public void SetString(string key, string value)
        {
            UnityEngine.PlayerPrefs.SetString(key, value);
        }

        public int GetInt(string key)
        {
            return UnityEngine.PlayerPrefs.GetInt(key);
        }

        public void SetInt(string key, int value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value);
        }

        public bool GetBool(string key)
        {
            return UnityEngine.PlayerPrefs.GetInt(key) == 1;
        }

        public void SetBool(string key, bool value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }
}
