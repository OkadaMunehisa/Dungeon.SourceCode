using Fusion;

namespace Dungeon.Characters
{
    public class StateMachine
    {
        // ���݂̏��
        [Networked]
        public IState CurrentState { get; set; }

        // �L�����N�^�[�̏��
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
            // ������Ԃ�ݒ�
            CurrentState = state;
            // ��Ԃ��J�n
            state.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            // �ߋ��̏�Ԃ��I��
            CurrentState.Exit();
            // ��Ԃ�ۑ�
            CurrentState = nextState;
            // �V�K��Ԃ��J�n
            nextState.Enter();
        }

        public void Update()
        {
            // ��Ԃ��X�V
            CurrentState?.Update();
        }
    }
}

