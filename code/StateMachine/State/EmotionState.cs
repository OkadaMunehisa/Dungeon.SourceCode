using UnityEngine;

namespace Dungeon.Characters
{
    public class EmotionState : IState
    {
        private CharacterCore _characterCore;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;

        public EmotionState(CharacterCore characterCore)
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
                // �G���[�g�J�n
                _characterCore.EmotionSystem.StartEmotionTimer(1.7f);
            }
        }

        public void Update()
        {
            // InputData���擾����
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // �G���[�g���I�����Ă���Ȃ�
                if (_characterCore.EmotionSystem.NotEmotionTimer)
                {
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
        }

        public void Exit() { }

        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            // �X�|�[���A�j���[�V�������J�E���g����
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Emotion);
        }
    }
}
