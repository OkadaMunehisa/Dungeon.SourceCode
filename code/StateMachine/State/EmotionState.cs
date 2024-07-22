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
            // 状態権限者
            if (_hasStateAuthority)
            {
                // アニメーションを再生
                AnimationTrigger();
                // エモート開始
                _characterCore.EmotionSystem.StartEmotionTimer(1.7f);
            }
        }

        public void Update()
        {
            // InputDataを取得する
            if (_characterCore.GetInput(out NetworkInputData input))
            {
                // エモートが終了しているなら
                if (_characterCore.EmotionSystem.NotEmotionTimer)
                {
                    if (input.MoveDirection != Vector2.zero)
                    {
                        // 状態変化 : 移動
                        _stateMachine.TransitionTo(_stateMachine.RunState);
                    }
                    else
                    {
                        // 状態変化 : 待機
                        _stateMachine.TransitionTo(_stateMachine.IdleState);
                    }
                }
            }
        }

        public void Exit() { }

        /// <summary> アニメーション </summary>
        private void AnimationTrigger()
        {
            // スポーンアニメーションをカウントする
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Emotion);
        }
    }
}
