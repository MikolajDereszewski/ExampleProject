using UnityEngine;

namespace Game.Logic
{
    /// <summary>
    /// Scriptable Object containing game settings
    /// </summary>
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game Scriptables/Game Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField]
        public int DefaultPlayerHealth { get; private set; }
        [SerializeField]
        private WaveSettings[] _waveSettings;

        /// <summary>
        /// Returns wave settings or last wave if index exceeds array length
        /// </summary>
        /// <param name="index">Wave index</param>
        /// <returns>Single wave settings</returns>
        public WaveSettings GetWaveOfIndex(int index)
        {
            if(index < 0 || index >= _waveSettings.Length)
            {
                return _waveSettings[_waveSettings.Length - 1];
            }
            return _waveSettings[index];
        }
    }
}