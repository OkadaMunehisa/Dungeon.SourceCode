using R3;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeon.UserInterface
{
    public class SessionCreateView : ViewBase
    {
        [Header("-Button-")]
        [SerializeField] Button _confirmButton;
        [SerializeField] Button _closeButton;

        [Header("-TMP_InputField-")]
        [SerializeField] TMP_InputField _nameEntryInputField;

        public Observable<Unit> OnConfirmButton => _confirmButton.OnClickAsObservable();
        public Observable<Unit> OnCloseButton => _closeButton.OnClickAsObservable();

        public Observable<string> NameEntryInputField => _nameEntryInputField.onValueChanged.AsObservable();

        private void Awake()
        {
            _nameEntryInputField.onValueChanged.AddListener(delegate { CheckInputField(); });
        }

        private void Reset()
        {
            _confirmButton.interactable = false;
            _nameEntryInputField.text = "";
        }

        private void CheckInputField()
        {
            // ‰pŒê“ü—Í‚Ì‚Ý && •¶Žš“ü—Í‚ª1ˆÈã‚ÌŽž
            if (Regex.IsMatch(_nameEntryInputField.text, @"^[a-zA-Z]*$") && _nameEntryInputField.text.Length > 0)
            {
                _confirmButton.interactable = true;
            }
            else
            {
                _confirmButton.interactable = false;
            }
        }

        /// <summary> •\Ž¦ </summary>
        public override void Show()
        {
            base.Show();

            Reset();
        }
    }
}
