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

        public void NextGeneration(CellManager manager)
        {
            var min = new Point { x = this.id.x - 1, y = this.id.y - 1 };
            var max = new Point { x = this.id.x + 1, y = this.id.y + 1 };
            for (var y = min.y; y <= max.y; ++y)
            {
                for (var x = min.x; x <= max.x; ++x)
                {
                    var targetId = new Point { x = x, y = y };
                    if (manager.ProcessedCells.ContainsKey(targetId))
                    {
                        continue;
                    }

                    var adjacentNumber = manager.GetAdjacentNumber(targetId);
                    var isAlive = manager.CellDictionary.ContainsKey(targetId);
                    if (isAlive && (adjacentNumber <= 1 || adjacentNumber >= 4))
                    {
                        manager.DeathCells.Add(targetId);
                    }
                    else if (!isAlive && adjacentNumber == 3)
                    {
                        manager.NewCells.Add(targetId);
                    }

                    manager.ProcessedCells.Add(targetId, false);
                }
            }

        }
    }
}
