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

        /// <summary> Button : キャラクター決定 </summary>
        public Observable<CharacterData> OnConfirmButton => _confirmEvent;

        /// <summary> キャラクターを確定します</summary>
        public void ConfirmCharacter(CharacterData characterData)
        {
            _confirmEvent.OnNext(characterData);
        }

        /// <summary> キャラクター選択リストを設定する </summary>
        public void UpdateCharacterSelect(List<CharacterData> characterDatas)
        {
            // 既存の内容を空にする
            foreach (Transform child in _viewPrefabContainer)
            {
                Destroy(child.gameObject);
            }

            // キャラクターデータリストを元に、UI Prefabを作成します
            foreach (CharacterData characterData in characterDatas)
            {
                // UIを作成します
                GameObject prefab = Instantiate(_viewPrefab, _viewPrefabContainer);
                // クラスを取得します
                SelectionPrefabView characterInfo = prefab.GetComponent<SelectionPrefabView>();
                // キャラクターデータを設定します
                characterInfo.SetSelectionPrefab(characterData, this);
            }
        }
    }
}

