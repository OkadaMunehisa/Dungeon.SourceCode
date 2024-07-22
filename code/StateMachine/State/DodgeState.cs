using UnityEngine;

namespace Dungeon.Characters
{
    public class DodgeState : IState
    {
        private CharacterCore _characterCore;
        private StateMachine _stateMachine => _characterCore.StateMachine;
        private DodgeSystem _dodgeSystem => _characterCore.DodgeSystem;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;

        public DodgeState(CharacterCore characterCore)
        {
            _characterCore = characterCore;
        }

        public void Enter()
        {
            if (_hasStateAuthority)
            {
                // アニメーションを再生
                DodgeAnimationTrigger();

                // InputDataを取得する
                if (_characterCore.GetInput(out NetworkInputData input))
                {
                    // 回避行動を開始
                    _dodgeSystem.DodgeStart(_characterCore.transform, input.MoveDirection, 20f);
                }
            }
        }

        public void Update()
        {
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // 回避行動が終了している場合
                if (_dodgeSystem.NotDodgeTimer)
                {
                    // Key : WASD
                    if (input.MoveDirection != Vector2.zero)
                    {
                        // 状態変化 : 移動
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    else
                    {
                        // 状態変化 : 待機
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }
            }

            // 回避行動の移動を実行します
            _dodgeSystem.DodgeMovement();
        }

        public void Exit()
        {
            // アニメーションを終了する
            DodgeExitAnimationTrigger();
            // 回避行動を終了する
            _dodgeSystem.DodgeStop(5f);
        }

        private void DodgeAnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Dodge);
        }

        private void DodgeExitAnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.DodgeExit);
        }
    }
}