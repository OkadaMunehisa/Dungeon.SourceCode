using R3;
using UnityEngine;

namespace Dungeon.UserInterface
{
    public class TitleTopModel : ModelBase, ITitleTopModel
    {
        private readonly Subject<Unit> _startEvent = new Subject<Unit>();
        private readonly Subject<Unit> _settingEvent = new Subject<Unit>();

        public Observable<Unit> StartEvent => _startEvent.AsObservable();
        public Observable<Unit> SettingEvent => _settingEvent.AsObservable();

        public void OnClickStart()
        {
            Debug.Log("OnClickStart");

            _startEvent.OnNext(Unit.Default);
            _disableEvent.OnNext(Unit.Default);
        }

        public void OnClickSetting()
        {
            Debug.Log("OnClickSetting");

            _settingEvent.OnNext(Unit.Default);
            _disableEvent.OnNext(Unit.Default);
        }

        public void OnClickClose()
        {
            Debug.Log("OnClickClose");

            _disableEvent.OnNext(Unit.Default);
        }

        /// <summary> —LŒø‰» </summary>
        public override void SetEnable()
        {
            base.SetEnable();
        }

        /// <summary> –³Œø‰» </summary>
        public override void SetDisable()
        {
            base.SetDisable();
        }
    }
}
