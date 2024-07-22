using R3;

namespace Dungeon.UserInterface
{
    /// <summary> ���f���̃x�[�X�N���X </summary>
    public abstract class ModelBase
    {
        protected readonly Subject<Unit> _enableEvent = new Subject<Unit>();
        protected readonly Subject<Unit> _disableEvent = new Subject<Unit>();

        public Observable<Unit> EnableEvent => _enableEvent;
        public Observable<Unit> DisableEvent => _disableEvent;

        /// <summary> �L���� </summary>
        public virtual void SetEnable()
        {
            _enableEvent.OnNext(Unit.Default);
        }

        /// <summary> ������ </summary>
        public virtual void SetDisable()
        {
            _disableEvent.OnNext(Unit.Default);
        }
    }
}
