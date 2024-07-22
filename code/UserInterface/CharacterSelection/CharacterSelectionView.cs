using Dungeon.Characters;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon.UserInterface
{
    public class CharacterSelectionView : ViewBase
    {
        [Header("-View PrefabContainer")]
        [SerializeField] Transform _viewPrefabContainer;

        [Header("-View Prefab-")]
        [SerializeField] GameObject _viewPrefab;

        private Subject<CharacterData> _confirmEvent = new Subject<CharacterData>();

        /// <summary> Button : �L�����N�^�[���� </summary>
        public Observable<CharacterData> OnConfirmButton => _confirmEvent;

        /// <summary> �L�����N�^�[���m�肵�܂�</summary>
        public void ConfirmCharacter(CharacterData characterData)
        {
            _confirmEvent.OnNext(characterData);
        }

        /// <summary> �L�����N�^�[�I�����X�g��ݒ肷�� </summary>
        public void UpdateCharacterSelect(List<CharacterData> characterDatas)
        {
            // �����̓��e����ɂ���
            foreach (Transform child in _viewPrefabContainer)
            {
                Destroy(child.gameObject);
            }

            // �L�����N�^�[�f�[�^���X�g�����ɁAUI Prefab���쐬���܂�
            foreach (CharacterData characterData in characterDatas)
            {
                // UI���쐬���܂�
                GameObject prefab = Instantiate(_viewPrefab, _viewPrefabContainer);
                // �N���X���擾���܂�
                SelectionPrefabView characterInfo = prefab.GetComponent<SelectionPrefabView>();
                // �L�����N�^�[�f�[�^��ݒ肵�܂�
                characterInfo.SetSelectionPrefab(characterData, this);
            }
        }
    }
}

