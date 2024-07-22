using R3;

namespace Dungeon.UserInterface
{
    /// <summary> モデルのベースクラス </summary>
    public abstract class ModelBase
    {
        protected readonly Subject<Unit> _enableEvent = new Subject<Unit>();
        protected readonly Subject<Unit> _disableEvent = new Subject<Unit>();

        public Observable<Unit> EnableEvent => _enableEvent;
        public Observable<Unit> DisableEvent => _disableEvent;

        /// <summary> 有効化 </summary>
        public virtual void SetEnable()
        {
            _enableEvent.OnNext(Unit.Default);
        }

        /// <summary> 無効化 </summary>
        public virtual void SetDisable()
        {
            _disableEvent.OnNext(Unit.Default);
        }
    }
}
