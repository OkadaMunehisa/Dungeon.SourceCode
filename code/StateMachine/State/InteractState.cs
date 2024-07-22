using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon.Characters
{
    public class InteractState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        public InteractState(CharacterCore characterCore)
        {
            _characterCore = characterCore;
        }

        public void Enter()
        {
            if (_hasStateAuthority)
            {
                // インタラクトアニメーション
                AnimationTrigger();
                // 切り替えタイマー
                _characterCore.InteractSystem.StartInteractTimer(1);
                // インタラクト処理を実行
                _characterCore.InteractSystem.InteractGetOverlapSphere();
            }
        }

        public void Update()
        {
            // インタラクト状態が終了しているか？
            if (_characterCore.InteractSystem.NotInteractTimer)
            {
                // InputDataを取得する
                if (_characterCore.GetInput(out NetworkInputData input))
                {
                    // 入力値が0以外 && 状態権限者
                    if (input.MoveDirection != Vector2.zero && _hasStateAuthority)
                    {
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    // 入力値が0 && 状態権限者
                    else if (input.MoveDirection == Vector2.zero && _hasStateAuthority)
                    {
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
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
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Interact);
        }
    }
}

