namespace Dungeon.Characters
{
    public class DamageState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        public DamageState(CharacterCore characterCore)
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
                // ��_���[�W�^�C�}�[���X�^�[�g
                _characterCore.DamageSystem.StartDamage(0.6f);
            }
        }

        public void Update()
        {
            // ��_���[�W��Ԃ��I�����Ă���
            if (_characterCore.DamageSystem.NotDamage)
            {
                // ��Ԍ�����
                if (_hasStateAuthority)
                {
                    // ��ԕω� : �ҋ@
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
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
            // �X�|�[���A�j���[�V�������J�E���g����
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Damage);
        }
    }
}

