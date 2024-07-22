namespace Dungeon.Characters
{
    public class SpawnState : IState
    {
        private CharacterCore _characterCore;

        private bool _hasStateAuthority => _characterCore.HasStateAuthority;
        private StateMachine _stateMachine => _characterCore.StateMachine;

        public SpawnState(CharacterCore characterCore)
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
                // �N�[���^�C����ݒ�
                SetSpawnCooldown(2f);
            }
        }

        public void Update()
        {
            // �X�|�[���^�C�}�[���I�����Ă���ꍇ
            if (_characterCore.SpawnSystem.NotSpawnTimer)
            {
                // ��Ԍ�����
                if (_hasStateAuthority)
                {
                    // ��ԕω� : �ҋ@
                    _stateMachine.TransitionTo(_stateMachine.IdleState);
                }
            }
        }

        public void Exit()
        {
            // ��Ԍ�����
            if (_hasStateAuthority)
            {
                // RPC_�J�����̒Ǐ]��ݒ�
                ConfigureCameraFollow();
            }
        }

        /// <summary> �A�j���[�V���� </summary>
        private void AnimationTrigger()
        {
            // �X�|�[���A�j���[�V�������J�E���g����
            _characterCore.AnimationSystem.AnimationTriggerAdd(AnimationType.Spawn);
        }

        // �J�����Ǐ]�̐ݒ���s��
        private void ConfigureCameraFollow()
        {
            // RPC_�J�����ݒ�
            _characterCore.CameraSystem.RPC_SetCharacterCamera();
        }

        // �X�|�[���^�C�}�[��ݒ肷��
        private void SetSpawnCooldown(float cooldownTime)
        {
            // �X�|�[���N�[���^�C����ݒ�
            _characterCore.SpawnSystem.StartSpawnTimer(cooldownTime);
        }
    }
}


