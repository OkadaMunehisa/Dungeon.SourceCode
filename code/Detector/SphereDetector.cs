using UnityEngine;

namespace Dungeon
{
    /// <summary> Sphere型のオブジェクト検知 </summary>
    public class SphereDetector : BaseDetector
    {
        [SerializeField] float _radius;

        /// <summary> 範囲内のオブジェクト情報を検知する </summary>
        public override DetectorResult DetectObject()
        {
            // 回転座標を取得する
            var rotatedPosition = CalculateRotatedCenter();

            int count = Runner.GetPhysicsScene()
                .OverlapSphere(rotatedPosition,
                _radius, _hit, _hittableMask,
                QueryTriggerInteraction.UseGlobal);

            var result = new DetectorResult(_hit, count);

            return result;
        }

        protected override void DrawGizmo()
        {
            Gizmos.color = Color.green;
            // 回転座標を取得する
            var rotatedPosition = CalculateRotatedCenter();
            Gizmos.DrawWireSphere(rotatedPosition, _radius);
        }
    }
}
