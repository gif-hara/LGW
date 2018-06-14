using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PoolableCell : ObjectPool<CellController>
    {
        public PoolableCell(CellController original)
            :base(original)
        {
        }
    }
}
