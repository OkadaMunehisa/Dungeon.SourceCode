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
            // ��Ԍ�����
            if (_hasStateAuthority )
            {
                // �A�j���[�V�������Đ�
                AnimationTrigger();
            }
        }

        public void Update()
        {
            // InputData���擾����
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // Key : Space && �ړ����͂��s���Ă��� && ����N�[���_�E�����I�����Ă���
                if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION0) && input.MoveDirection != Vector2.zero && _dodgeSystem.NotCooldownTimer)
                {
                    // ��ԕω� : ���
                    _stateMachine.TransitionTo(_stateMachine.DodgeState);
                }
                // Mouse : LeftClick
                else if (input.Buttons.WasReleased(_prevButtons, NetworkInputData.BUTTON_ACTION1))
                {
                    // ��ԕω� : �U��
                    _stateMachine.TransitionTo(_stateMachine.AttackState);
                }
                // Key : E
                else if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION2))
                {
                    // ��ԕω� : �C���^���N�g
                    _stateMachine.TransitionTo(_stateMachine.InteractState);
                }
                // Key : F
                else if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION3))
                {
                    // ��ԕω� : �G���[�V����
                    _stateMachine.TransitionTo(_stateMachine.EmotionState);
                }
                // Key : WASD
                else if (input.MoveDirection != Vector2.zero)
                {
                    // ��ԕω� : �ړ�
                    _stateMachine.TransitionTo(_stateMachine.RunState);
                }
            }

            // �L�����N�^�[�R���g���[���𓯊�������
            _characterCore.MovementSystem.BaseMovement();
            // �{�^������ۑ�
            _prevButtons = input.Buttons;
        }

        public void Exit() { }

        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Idle);
        }
    }
}
