using UnityEngine;

namespace Dungeon.UserInterface
{
    /// <summary> ビューのベースクラス </summary>
    public abstract class ViewBase : MonoBehaviour
    {
        [Header("-View Object-")]
        [SerializeField] protected GameObject _viewObject;

        /// <summary> 表示 </summary>
        public virtual void Show()
        {
            _viewObject?.SetActive(true);
        }

        /// <summary> 非表示 </summary>
        public virtual void Hide()
        {
            _viewObject?.SetActive(false);
        }
    }
}
