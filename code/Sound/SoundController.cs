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

        /// <summary> �T�E���h�f�[�^�����݂��邩 </summary>
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

            // �f�[�^�̕ۑ�������߂܂�
            _soundDataPath = Application.dataPath + "/" + _folderName + "/" + _soundName;
            // �T�E���h�f�[�^���A�t�@�C�����烍�[�h���܂�
            LoadSoundData();
        }

        /// <summary> 0~1�̊Ԃɕ␳�����s���āA�Q�Ƃ�ۑ����܂� </summary>
        public void SetSoundData(SoundType soundType, float Volume)
        {
            // �������� : �T�E���h�f�[�^�����݂��Ȃ�
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

        /// <summary> �T�E���h�f�[�^���AJson �ɕϊ����āA�t�H���_�[�ɕۑ����܂� </summary>
        public void SaveSoundData()
        {
            // �������� : �T�E���h�f�[�^�����݂��Ȃ�
            if (!IsSoundDataActive) return;

            JsonDataController.SaveData(_soundData, _soundDataPath);
        }

        /// <summary> Json ����ϊ����āA�T�E���h�f�[�^���擾���܂� </summary>
        public void LoadSoundData()
        {
            _soundData = JsonDataController.LoadData<SoundData>(_soundDataPath);
        }

        /// <summary> �T�E���h�f�[�^���A�����l�Ƀ��Z�b�g���܂� </summary>
        public void ResetSoundData()
        {
            if (!IsSoundDataActive) return;

            _soundData = new SoundData();
        }

        /// <summary> ���݂̃T�E���h�f�[�^���擾���܂� </summary>
        public SoundData GetSoundData()
        {
            if (!IsSoundDataActive) return default;

            return _soundData;
        }
    }
}
