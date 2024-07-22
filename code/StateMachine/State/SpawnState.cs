namespace Dungeon.Characters
{
    public class SpawnState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        public SpawnState(CharacterCore characterCore)
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
                // クールタイムを設定
                SetSpawnCooldown(2f);
            }
        }

        public void Update()
        {
            // スポーンタイマーが終了している場合
            if (_characterCore.SpawnSystem.NotSpawnTimer)
            {
                // 状態権限者
                if (_hasStateAuthority)
                {
                    // 状態変化 : 待機
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
                }
            }
        }

        public void Exit()
        {
            // 状態権限者
            if (_hasStateAuthority)
            {
                // RPC_カメラの追従を設定
                ConfigureCameraFollow();
            }
        }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            // スポーンアニメーションをカウントする
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Spawn);
        }

        // カメラ追従の設定を行う
        private void ConfigureCameraFollow()
        {
            // RPC_カメラ設定
            _characterCore.CameraSystem.RPC_SetCharacterCamera();
        }

        // スポーンタイマーを設定する
        private void SetSpawnCooldown(float cooldownTime)
        {
            // スポーンクールタイムを設定
            _characterCore.SpawnSystem.StartSpawnTimer(cooldownTime);
        }
    }
}


