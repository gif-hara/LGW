using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// <see cref="CellController"/>を管理するクラス
    /// </summary>
    public sealed class CellManager : MonoBehaviour
    {
        [SerializeField]
        private CellController cellPrefab;

        private readonly List<CellController> cellList = new List<CellController>();

        private readonly Dictionary<Point, CellController> cellDictionary = new Dictionary<Point, CellController>();

        private readonly List<Point> newCells = new List<Point>();

        private readonly List<Point> deathCells = new List<Point>();

        private PoolableCell poolableCell;

        private void Awake()
        {
            this.poolableCell = new PoolableCell(this.cellPrefab);
        }

        private void Update()
        {
            for(var i=0; i<10; i++)
            {
                var keyCode = (KeyCode)((int)KeyCode.Alpha1 + i);
                if(Input.GetKeyDown(keyCode))
                {
                    this.ToggleCell(new Point { x = i, y = 0 });
                }
            }
        }

        public void NextGeneratioin()
        {
        }

        public void CreateCell(Point id)
        {
            Assert.IsFalse(this.cellDictionary.ContainsKey(id), $"id = {id}にセルが存在するのに生成しようとしました");
            var cell = this.poolableCell.Rent();
            cell.Id = id;
            this.cellList.Add(cell);
            this.cellDictionary.Add(id, cell);
        }

        public void RemoveCell(Point id)
        {
            Assert.IsTrue(this.cellDictionary.ContainsKey(id), $"id = {id}にセルが無いのに削除しようとしました");
            var cell = this.cellDictionary[id];
            this.poolableCell.Return(cell);
            this.cellList.Remove(cell);
            this.cellDictionary.Remove(id);
        }

        public void ToggleCell(Point id)
        {
            if(this.cellDictionary.ContainsKey(id))
            {
                this.RemoveCell(id);
            }
            else
            {
                this.CreateCell(id);
            }
        }
    }
}
