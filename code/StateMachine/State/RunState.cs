using Fusion;
using UnityEngine;

namespace Dungeon.Characters
{
    public class RunState : IState
    {
        private CharacterCore _characterCore;
        private StateMachine _stateMachine => _characterCore.StateMachine;
        private DodgeSystem _dodgeSystem => _characterCore.DodgeSystem;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        

        private NetworkButtons _prevButtons;

        public RunState(CharacterCore characterCore)
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
            }
        }

        public void Update()
        {
            // InputDataを取得する
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // Key : Space && 回避クールダウンが終了している
                if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION0) && _dodgeSystem.NotCooldownTimer)
                {
                    // 状態変化 : 回避
                    _stateMachine.TransitionTo(_stateMachine.DodgeState);
                }
                // Mouse : LeftClick
                else if (input.Buttons.WasReleased(_prevButtons, NetworkInputData.BUTTON_ACTION1))
                {
                    // 状態変化 : 攻撃
                    _stateMachine.TransitionTo(_stateMachine.AttackState);
                }
                // Key : E
                else if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION2))
                {
                    // 状態変化 : インタラクト
                    _stateMachine.TransitionTo(_stateMachine.InteractState);
                }
                // Key : F
                else if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION3))
                {
                    // 状態変化 : エモーション
                    _stateMachine.TransitionTo(_stateMachine.EmotionState);
                }
                // Key : WASD
                else if (input.MoveDirection == Vector2.zero)
                {
                    // 状態変化 : 待機
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
                }
                else
                {
                    // 移動処理を実行 // 後に、キャラクターパラメータから回転速度を取得する必要あり
                    _characterCore.MovementSystem.RunMovement(_characterCore.transform, input.MoveDirection, 10f);
                }
            }

            // 基礎移動を実行
            _characterCore.MovementSystem.BaseMovement();

            // ボタン情報を保存
            _prevButtons = input.Buttons;
        }

        public void Exit()
        {
            // 移動状態 -> エモート状態 -> 待機状態 に移動する際に、一瞬 Run状態が視認できる問題が発生するので、終了時に強制的にIdleを再生する
            RunStopAnimationTrigger();
        }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Run);
        }

        private void RunStopAnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Idle);
        }
    }
}

