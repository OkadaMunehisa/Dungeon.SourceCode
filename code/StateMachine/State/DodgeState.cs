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
                // �A�j���[�V�������Đ�
                DodgeAnimationTrigger();

                // InputData���擾����
                if (_characterCore.GetInput(out NetworkInputData input))
                {
                    // ����s�����J�n
                    _dodgeSystem.DodgeStart(_characterCore.transform, input.MoveDirection, 20f);
                }
            }
        }

        public void Update()
        {
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // ����s�����I�����Ă���ꍇ
                if (_dodgeSystem.NotDodgeTimer)
                {
                    // Key : WASD
                    if (input.MoveDirection != Vector2.zero)
                    {
                        // ��ԕω� : �ړ�
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    else
                    {
                        // ��ԕω� : �ҋ@
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }
            }

            // ����s���̈ړ������s���܂�
            _dodgeSystem.DodgeMovement();
        }

        public void Exit()
        {
            // �A�j���[�V�������I������
            DodgeExitAnimationTrigger();
            // ����s�����I������
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