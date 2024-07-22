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
            // ��Ԍ�����
            if (_hasStateAuthority)
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
                // Key : Space && ����N�[���_�E�����I�����Ă���
                if (input.Buttons.WasPressed(_prevButtons, NetworkInputData.BUTTON_ACTION0) && _dodgeSystem.NotCooldownTimer)
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
                else if (input.MoveDirection == Vector2.zero)
                {
                    // ��ԕω� : �ҋ@
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
                }
                else
                {
                    // �ړ����������s // ��ɁA�L�����N�^�[�p�����[�^�����]���x���擾����K�v����
                    _characterCore.MovementSystem.RunMovement(_characterCore.transform, input.MoveDirection, 10f);
                }
            }

            // ��b�ړ������s
            _characterCore.MovementSystem.BaseMovement();

            // �{�^������ۑ�
            _prevButtons = input.Buttons;
        }

        public void Exit()
        {
            // �ړ���� -> �G���[�g��� -> �ҋ@��� �Ɉړ�����ۂɁA��u Run��Ԃ����F�ł����肪��������̂ŁA�I�����ɋ����I��Idle���Đ�����
            RunStopAnimationTrigger();
        }

        /// <summary> �A�j���[�V���� </summary>
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

