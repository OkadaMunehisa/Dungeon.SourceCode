using UnityEngine;

namespace Dungeon
{
    /// <summary> 衝突検出の結果 </summary>
    public class DetectorResult
    {
        // 衝突したコライダーの配列
        private readonly Collider[] _hitColliders;
        // 衝突したコライダーの数
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
