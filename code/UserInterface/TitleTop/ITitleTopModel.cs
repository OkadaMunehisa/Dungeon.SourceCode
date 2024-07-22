using R3;
using UnityEngine.UI;

namespace Dungeon.UserInterface
{
    public interface ITitleTopModel
    {
        public Observable<Unit> StartEvent { get; }
        public Observable<Unit> SettingEvent { get; }

        public Observable<Unit> EnableEvent { get; }
        public Observable<Unit> DisableEvent { get; }

        public void OnClickStart();
        public void OnClickSetting();
        public void OnClickClose();

        public void SetEnable();
        public void SetDisable();
    }
}
