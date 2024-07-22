using Dungeon.Characters;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeon.UserInterface
{
    public class SelectionPrefabView : MonoBehaviour
    {
        [Header("-Button-")]
        [SerializeField] Button _confirmButton;

        [Header("-TextMeshProUGUI-")]
        [SerializeField] TextMeshProUGUI _nameText;

        [Header("-Image-")]
        [SerializeField] Image _Image;

        private CharacterData _characterData;

        private Observable<Unit> _onConfirmButton => _confirmButton.OnClickAsObservable();

        public void SetSelectionPrefab(CharacterData characterData, CharacterSelectionView characterSelectionView)
        {
            // キャラクターデータを保持
            _characterData = characterData;

            _nameText.text = characterData.CharacterName;
            _Image.sprite = _characterData.CharacterSprite;

            _onConfirmButton.Subscribe(_ =>
            {
                characterSelectionView.ConfirmCharacter(_characterData);
            }).AddTo(this);
        }
    }
}


