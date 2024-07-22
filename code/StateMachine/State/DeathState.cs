using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon.Characters
{
    public class DeathState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;

        public DeathState(CharacterCore characterCore)
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

        }

        public void Exit()
        {

        }

        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            // �X�|�[���A�j���[�V�������J�E���g����
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Damage);
        }
    }
}
