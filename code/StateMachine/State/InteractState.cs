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
                // �C���^���N�g�A�j���[�V����
                AnimationTrigger();
                // �؂�ւ��^�C�}�[
                _characterCore.InteractSystem.StartInteractTimer(1);
                // �C���^���N�g���������s
                _characterCore.InteractSystem.InteractGetOverlapSphere();
            }
        }

        public void Update()
        {
            // �C���^���N�g��Ԃ��I�����Ă��邩�H
            if (_characterCore.InteractSystem.NotInteractTimer)
            {
                // InputData���擾����
                if (_characterCore.GetInput(out NetworkInputData input))
                {
                    // ���͒l��0�ȊO && ��Ԍ�����
                    if (input.MoveDirection != Vector2.zero && _hasStateAuthority)
                    {
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    // ���͒l��0 && ��Ԍ�����
                    else if (input.MoveDirection == Vector2.zero && _hasStateAuthority)
                    {
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }
            }

            // �L�����N�^�[�R���g���[���𓯊�������
            _characterCore.MovementSystem.BaseMovement();
        }

        public void Exit()
        {

        }

        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Interact);
        }
    }
}

