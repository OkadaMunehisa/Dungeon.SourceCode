using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Dungeon.UserInterface
{
    public class TitleTopView : ViewBase
    {
        [Header("-Button-")]
        [SerializeField] Button _startButton;
        [SerializeField] Button _settingButton;
        [SerializeField] Button _closeButton;

        public Observable<Unit> OnStartButton => _startButton.OnClickAsObservable();
        public Observable<Unit> OnSettingButton => _settingButton.OnClickAsObservable();
        public Observable<Unit> OnCloseButton => _closeButton.OnClickAsObservable();

    }
}

