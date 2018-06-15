using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PoolableCell : ObjectPool<CellController>
    {
        private Transform parent;

        private CellController original;

        public PoolableCell(CellController original, Transform parent)
        {
            this.parent = parent;
            this.original = original;
        }

        protected override CellController CreateInstance()
        {
            return Object.Instantiate(this.original, this.parent);
        }
    }
}
