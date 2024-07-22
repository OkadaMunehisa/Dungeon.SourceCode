using Fusion;
using UnityEngine;

namespace Dungeon
{
    /// <summary> 接触検知の基盤 </summary>
    public abstract class BaseDetector : NetworkBehaviour, IDetector
    {
        [SerializeField] protected LayerMask _hittableMask;
        [SerializeField] protected Vector3 _center;

        protected Collider[] _hit = new Collider[5];

        /// <summary> 範囲内のオブジェクト情報を検知します </summary>
        public abstract DetectorResult DetectObject();

        /// <summary> 回転された中心座標を計算します </summary>
        protected Vector3 CalculateRotatedCenter()
        {
            // 現在のオブジェクトの回転を取得
            Quaternion rotation = transform.rotation;

            // 回転した後の中心座標を計算
            Vector3 rotatedCenter = rotation * _center;

            // 回転した後の座標を計算
            return transform.position + rotatedCenter;
        }

        protected abstract void DrawGizmo();

        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
    }
}
