using UnityEngine;

namespace Dungeon
{
    /// <summary> Box�^�̃I�u�W�F�N�g���m </summary>
    public class BoxDetector : BaseDetector
    {
        [SerializeField] Vector3 _size;

        /// <summary> �͈͓��̃I�u�W�F�N�g�������m���� </summary>
        public override DetectorResult DetectObject()
        {
            // ��]���W���擾����
            var rotatedPosition = CalculateRotatedCenter();

            int count = Runner.GetPhysicsScene().OverlapBox(rotatedPosition,
                _size / 2, _hit, transform.rotation, _hittableMask,
                QueryTriggerInteraction.UseGlobal);

            var result = new DetectorResult(_hit, count);

            return result;
        }

        protected override void DrawGizmo()
        {
            Gizmos.color = Color.green;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            // ��]���W���擾����
            var rotatedPosition = CalculateRotatedCenter();
            Gizmos.matrix = Matrix4x4.TRS(rotatedPosition, transform.rotation, Vector3.one);

            Gizmos.DrawWireCube(Vector3.zero, _size);

            Gizmos.matrix = oldMatrix;
        }
    }
}
