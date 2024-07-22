using UnityEngine;

namespace Dungeon
{
    /// <summary> �Փˌ��o�̌��� </summary>
    public class DetectorResult
    {
        // �Փ˂����R���C�_�[�̔z��
        private readonly Collider[] _hitColliders;
        // �Փ˂����R���C�_�[�̐�
        private readonly int _hitCount;

        public Collider[] HitColliders => _hitColliders;
        public int HitCount => _hitCount;

        public DetectorResult(Collider[] hitColliders, int hitCount)
        {
            _hitColliders = hitColliders;
            _hitCount = hitCount;
        }
    }
}
