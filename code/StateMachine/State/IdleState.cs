using Fusion;
using UnityEngine;

namespace Dungeon.Characters
{
    public class IdleState : IState
    {
        private CharacterCore _characterCore;
        private StateMachine _stateMachine => _characterCore.StateMachine;
        private DodgeSystem _dodgeSystem => _characterCore.DodgeSystem;

        private NetworkButtons _prevButtons;
        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        

        public IdleState(CharacterCore characterCore)
        {
            _characterCore = characterCore;
        }

        public void Enter()
        {
            // 状態権限者
            if (_hasStateAuthority )
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
                // Key : Space && 移動入力が行われている && 回避クールダウンが終了している
                if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION0) && input.MoveDirection != Vector2.zero && _dodgeSystem.NotCooldownTimer)
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
                else if (input.MoveDirection != Vector2.zero)
                {
                    // 状態変化 : 移動
                    _stateMachine.TransitionTo(_stateMachine.RunState);
                }
            }

            // キャラクターコントローラを同期させる
            _characterCore.MovementSystem.BaseMovement();
            // ボタン情報を保存
            _prevButtons = input.Buttons;
        }

        public void Exit() { }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Idle);
        }
    }
}
