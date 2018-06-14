using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CellController : MonoBehaviour
    {
        private Transform cachedTransform;

        private Point id;
        public Point Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                var size = UserSettings.Instance.CellSize;
                this.cachedTransform.localPosition = new Vector3(this.id.x * size, this.id.y * size);
            }
        }

        private void Awake()
        {
            this.cachedTransform = this.transform;
            this.cachedTransform.localScale = Vector3.one * UserSettings.Instance.CellSize;
        }
    }
}
