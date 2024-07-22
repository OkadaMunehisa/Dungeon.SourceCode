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
            // 状態権限者
            if (_hasStateAuthority)
            {
                // アニメーションを再生
                AnimationTrigger();
            }
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            // スポーンアニメーションをカウントする
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Damage);
        }
    }
}
