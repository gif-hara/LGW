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

        private void Awake()
        {
            this.cachedTransform = this.transform;
            this.cachedTransform.localScale = Vector3.one * UserSettings.Instance.CellSize;
        }
    }
}
