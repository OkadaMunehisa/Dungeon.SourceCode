using UnityEngine;

namespace Dungeon.UserInterface
{
    /// <summary> �r���[�̃x�[�X�N���X </summary>
    public abstract class ViewBase : MonoBehaviour
    {
        [Header("-View Object-")]
        [SerializeField] protected GameObject _viewObject;

        /// <summary> �\�� </summary>
        public virtual void Show()
        {
            _viewObject?.SetActive(true);
        }

        /// <summary> ��\�� </summary>
        public virtual void Hide()
        {
            _viewObject?.SetActive(false);
        }
    }
}
