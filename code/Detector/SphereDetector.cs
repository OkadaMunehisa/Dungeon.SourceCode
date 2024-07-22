using UnityEngine;

namespace Dungeon
{
    /// <summary> Sphere�^�̃I�u�W�F�N�g���m </summary>
    public class SphereDetector : BaseDetector
    {
        [SerializeField] float _radius;

        /// <summary> �͈͓��̃I�u�W�F�N�g�������m���� </summary>
        public override DetectorResult DetectObject()
        {
            // ��]���W���擾����
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
            // ��]���W���擾����
            var rotatedPosition = CalculateRotatedCenter();
            Gizmos.DrawWireSphere(rotatedPosition, _radius);
        }
    }
}
