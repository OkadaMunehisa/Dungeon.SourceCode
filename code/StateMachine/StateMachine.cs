using Fusion;

namespace Dungeon.Characters
{
    public class StateMachine
    {
        // 現在の状態
        [Networked]
        public IState CurrentState { get; set; }

        // キャラクターの状態
        public IState SpawnState { get; set; }
        public IState IdleState { get; set; }
        public IState RunState { get; set; }
        public IState DodgeState { get; }
        public IState AttackState { get; set; }
        public IState InteractState { get; set; }
        public IState DamageState { get; set; }
        public IState DeathState { get; set; }
        public IState EmotionState { get; set; }

        public StateMachine(CharacterCore characterCore)
        {
            SpawnState = new SpawnState(characterCore);
            IdleState = new IdleState(characterCore);
            RunState = new RunState(characterCore);
            DodgeState = new DodgeState(characterCore);
            AttackState = new AttackState(characterCore);
            InteractState = new InteractState(characterCore);
            DamageState = new DamageState(characterCore);
            DeathState = new DeathState(characterCore);
            EmotionState = new EmotionState(characterCore);
        }

        public void Initialize(IState state)
        {
            // 初期状態を設定
            CurrentState = state;
            // 状態を開始
            state.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            // 過去の状態を終了
            CurrentState.Exit();
            // 状態を保存
            CurrentState = nextState;
            // 新規状態を開始
            nextState.Enter();
        }

        public void Update()
        {
            // 状態を更新
            CurrentState?.Update();
        }
    }
}

