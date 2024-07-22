using Fusion;
using System.Collections;
using UnityEngine;

namespace Dungeon.Trap
{
    public class SpikesTrap : NetworkBehaviour
    {
        [SerializeField] int _trapDamage = 15;
        [SerializeField] float _trapCooldownTime = 10f;
        [SerializeField] float _createDamageTime = 0.5f;

        // ���͂����m����
        [SerializeField] BoxDetector _detector;

        // �g���b�v�̃A�j���[�V�������Đ�����
        [SerializeField] TrapAnimation _trapAnimation;

        // �N�[���_�E�����v������
        private CooldownTimer _cooldown;
        // �g���b�v�̗L���A�����𔻒肷��
        private TrapActive _trapActive;

        public override void Spawned()
        {
            _cooldown = new CooldownTimer(Runner);
            _trapActive = new TrapActive();

            if (HasStateAuthority)
            {
                // �g���b�v��L���ɂ���
                _trapActive.ActivateTrap();
            }
        }

        public override void FixedUpdateNetwork()
        {
            // ��Ԍ����҈ȊO
            if (!HasStateAuthority)
                return;

            // �g���b�v���L���ȏꍇ
            if (_trapActive.IsTrapActive)
            {
                // �N�[���^�C�������I�����Ă���Ȃ�
                if (_cooldown.IsNotCooldownActive)
                {
                    // �g���b�v�̎��Ӄv���C���[�����m
                    var detectorResult = _detector.DetectObject();

                    // �ڐG�\�I�u�W�F�N�g���P�ȏ㑶�݂���ꍇ
                    if (detectorResult.HitCount > 0)
                    {
                        // �A�j���[�V�������J�n
                        _trapAnimation.AnimationTrigger();

                        // �_���[�W�����̗\��
                        StartCoroutine(DealDamageAfterDelay(_createDamageTime));
                        // �N�[���^�C�����X�^�[�g
                        _cooldown.StartCooldown(_trapCooldownTime);
                    }
                }
            }
        }

        private IEnumerator DealDamageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            // �g���b�v�̎��Ӄv���C���[�����m
            var detectorResult = _detector.DetectObject();

            // �ڐG�\�I�u�W�F�N�g���P�ȏ㑶�݂���ꍇ
            if (detectorResult.HitCount > 0)
            {
                for (int i = 0; i < detectorResult.HitCount; i++)
                {
                    IDamageable damageApplicable = detectorResult.HitColliders[i].GetComponent<IDamageable>();

                    if (damageApplicable != null)
                    {
                        // �_���[�W��^����
                        damageApplicable.ApplyDamage(_trapDamage);
                    }
                }
            }
        }
    }
}
