namespace Dungeon.Characters
{
    public class DamageState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        public DamageState(CharacterCore characterCore)
        {
            _characterCore = characterCore;
        }

        public void Enter()
        {
            // 状態権限者
            if (_hasStateAuthority)
            {
                // アニメーションを再生
                AnimationTrigger();
                // 被ダメージタイマーをスタート
                _characterCore.DamageSystem.StartDamage(0.6f);
            }
        }

        public void Update()
        {
            // 被ダメージ状態が終了している
            if (_characterCore.DamageSystem.NotDamage)
            {
                // 状態権限者
                if (_hasStateAuthority)
                {
                    // 状態変化 : 待機
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
                }
            }

            // キャラクターコントローラを同期させる
            _characterCore.MovementSystem.BaseMovement();
        }

        public void Exit()
        {

        }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            // スポーンアニメーションをカウントする
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Damage);
        }
    }
}

