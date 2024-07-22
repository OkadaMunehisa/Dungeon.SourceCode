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
            // ��Ԍ�����
            if (_hasStateAuthority)
            {
                // �A�j���[�V�������Đ�
                AnimationTrigger();
                // �U�������̃A�V�X�g���s��
                _characterCore.MovementSystem.SupportRotation(_characterCore.transform);
                // �U���������J�n
                _characterCore.AttackSystem.OnAttack();
                // ���͗�����������
                _nextComboButton = false;
            }
        }

        public void Update()
        {
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // �R���{���͂��ő�ɒB���Ă��Ȃ�
                if(_attackSystem.IsMaxComboReached == false)
                {
                    // �v���o�b�t�@��t���ԓ�
                    if (_attackSystem.IsPreBufferActive)
                    {
                        // ���N���b�N�����͂���Ă���Ȃ�
                        if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION1))
                        {
                            // ��s���͂��s��ꂽ
                            _nextComboButton = true;
                        }
                    }
                }

                // -----------------------------------------------------------

                // �U�����I�����Ă���ꍇ
                if (_characterCore.AttackSystem.NotAttackTimer)
                {
                    // ��s���͂��s���Ă��� && �ő�R���{���ɓ��B���Ă��Ȃ�
                    if(_nextComboButton && _attackSystem.IsMaxComboReached == false)
                    {
                        // ��ԕω� : �ړ�
                        _stateMachine.TransitionTo(_stateMachine.AttackState);
                    }
                    // Input MoveDirection : �ړ�
                    else if (input.MoveDirection != Vector2.zero)
                    {
                        // �U���I���̃A�j���[�V�������Đ�
                        AttackEndTrigger();
                        // �U���̃N�[���_�E����ݒ�
                        _characterCore.AttackSystem.StartCooldownTimer(1.0f);
                        // �R���{�񐔂����Z�b�g����
                        _attackSystem.ResetComboCount();
                        // ��ԕω� : �ړ�
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    // Input MoveDirection : �ҋ@
                    else if (input.MoveDirection == Vector2.zero)
                    {
                        // �U���I���̃A�j���[�V�������Đ�
                        AttackEndTrigger();
                        // �U���̃N�[���_�E����ݒ�
                        _characterCore.AttackSystem.StartCooldownTimer(1.0f);
                        // �R���{�񐔂����Z�b�g����
                        _attackSystem.ResetComboCount();
                        // ��ԕω� : �ҋ@
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }

                // �L�����N�^�[�R���g���[���𓯊�������
                _characterCore.MovementSystem.BaseMovement();
            }
        }

        public void Exit()
        {

        }


        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Attack);
        }

        /// <summary> �U���I���A�j���[�V���� </summary>
        private void AttackEndTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.AttackEnd);
        }
    }
}

