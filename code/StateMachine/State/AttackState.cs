using Fusion;
using UnityEngine;

namespace Dungeon.Characters
{
    public class AttackState : IState
    {
        private CharacterCore _characterCore;

        private IAttackSystem _attackSystem => _characterCore.AttackSystem;
        private StateMachine _stateMachine => _characterCore.StateMachine;
        private bool _hasStateAuthority => _characterCore.HasStateAuthority;

        private NetworkBool _nextComboButton;
        private NetworkButtons _prevButtons;

        public AttackState(CharacterCore characterCore)
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
                // 攻撃方向のアシストを行う
                _characterCore.MovementSystem.SupportRotation(_characterCore.transform);
                // 攻撃処理を開始
                _characterCore.AttackSystem.OnAttack();
                // 入力履歴を初期化
                _nextComboButton = false;
            }
        }

        public void Update()
        {
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // コンボ入力が最大に達していない
                if(_attackSystem.IsMaxComboReached == false)
                {
                    // プレバッファ受付時間内
                    if (_attackSystem.IsPreBufferActive)
                    {
                        // 左クリックが入力されているなら
                        if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION1))
                        {
                            // 先行入力が行われた
                            _nextComboButton = true;
                        }
                    }
                }

                // -----------------------------------------------------------

                // 攻撃が終了している場合
                if (_characterCore.AttackSystem.NotAttackTimer)
                {
                    // 先行入力が行われている && 最大コンボ数に到達していない
                    if(_nextComboButton && _attackSystem.IsMaxComboReached == false)
                    {
                        // 状態変化 : 移動
                        _stateMachine.TransitionTo(_stateMachine.AttackState);
                    }
                    // Input MoveDirection : 移動
                    else if (input.MoveDirection != Vector2.zero)
                    {
                        // 攻撃終了のアニメーションを再生
                        AttackEndTrigger();
                        // 攻撃のクールダウンを設定
                        _characterCore.AttackSystem.StartCooldownTimer(1.0f);
                        // コンボ回数をリセットする
                        _attackSystem.ResetComboCount();
                        // 状態変化 : 移動
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    // Input MoveDirection : 待機
                    else if (input.MoveDirection == Vector2.zero)
                    {
                        // 攻撃終了のアニメーションを再生
                        AttackEndTrigger();
                        // 攻撃のクールダウンを設定
                        _characterCore.AttackSystem.StartCooldownTimer(1.0f);
                        // コンボ回数をリセットする
                        _attackSystem.ResetComboCount();
                        // 状態変化 : 待機
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }

                // キャラクターコントローラを同期させる
                _characterCore.MovementSystem.BaseMovement();
            }
        }

        public void Exit()
        {

        }


        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Attack);
        }

        /// <summary> 攻撃終了アニメーション </summary>
        private void AttackEndTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.AttackEnd);
        }
    }
}

