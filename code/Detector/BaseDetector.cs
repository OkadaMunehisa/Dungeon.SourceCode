using Fusion;
using UnityEngine;

namespace Dungeon
{
    /// <summary> �ڐG���m�̊�� </summary>
    public abstract class BaseDetector : NetworkBehaviour, IDetector
    {
        [SerializeField] protected LayerMask _hittableMask;
        [SerializeField] protected Vector3 _center;

        protected Collider[] _hit = new Collider[5];

        /// <summary> �͈͓��̃I�u�W�F�N�g�������m���܂� </summary>
        public abstract DetectorResult DetectObject();

        /// <summary> ��]���ꂽ���S���W���v�Z���܂� </summary>
        protected Vector3 CalculateRotatedCenter()
        {
            // ���݂̃I�u�W�F�N�g�̉�]���擾
            Quaternion rotation = transform.rotation;

            // ��]������̒��S���W���v�Z
            Vector3 rotatedCenter = rotation * _center;

            // ��]������̍��W���v�Z
            return transform.position + rotatedCenter;
        }

        protected abstract void DrawGizmo();

        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
    }
}
