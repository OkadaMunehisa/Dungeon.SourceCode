using UnityEngine;

namespace Dungeon.Sound
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;

        [SerializeField] SoundData _soundData = new SoundData();

        private string _soundDataPath;
        private string _soundName = "SoundData";
        private string _folderName = "JsonSavaData";

        public float MasterVolume => _soundData.MasterVolume;
        public float MusicVolume => _soundData.MusicVolume;
        public float SfxVolume => _soundData.SfxVolume;

        /// <summary> サウンドデータが存在するか </summary>
        private bool IsSoundDataActive => _soundData != null;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // データの保存先を求めます
            _soundDataPath = Application.dataPath + "/" + _folderName + "/" + _soundName;
            // サウンドデータを、ファイルからロードします
            LoadSoundData();
        }

        /// <summary> 0~1の間に補正を実行して、参照を保存します </summary>
        public void SetSoundData(SoundType soundType, float Volume)
        {
            // 条件分岐 : サウンドデータが存在しない
            if (!IsSoundDataActive) return;

            float fixVolume = Mathf.Clamp01(Volume);

            switch (soundType)
            {
                case SoundType.MASTER:
                    _soundData.MasterVolume = fixVolume;
                    break;

                case SoundType.MUSIC:
                    _soundData.MusicVolume = fixVolume;
                    break;

                case SoundType.SFX:
                    _soundData.SfxVolume = fixVolume;
                    break;
            }
        }

        /// <summary> サウンドデータを、Json に変換して、フォルダーに保存します </summary>
        public void SaveSoundData()
        {
            // 条件分岐 : サウンドデータが存在しない
            if (!IsSoundDataActive) return;

            JsonDataController.SaveData(_soundData, _soundDataPath);
        }

        /// <summary> Json から変換して、サウンドデータを取得します </summary>
        public void LoadSoundData()
        {
            _soundData = JsonDataController.LoadData<SoundData>(_soundDataPath);
        }

        /// <summary> サウンドデータを、初期値にリセットします </summary>
        public void ResetSoundData()
        {
            if (!IsSoundDataActive) return;

            _soundData = new SoundData();
        }

        /// <summary> 現在のサウンドデータを取得します </summary>
        public SoundData GetSoundData()
        {
            if (!IsSoundDataActive) return default;

            return _soundData;
        }
    }
}
