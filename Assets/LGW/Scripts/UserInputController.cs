using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UserInputController : MonoBehaviour
    {
        [SerializeField]
        private CellManager cellManager;

        [SerializeField]
        private Camera controlledCamera;

        private Vector3 dragPosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.cellManager.NextGeneratioin();
            }
            if (Input.GetKey(KeyCode.Return))
            {
                this.cellManager.NextGeneratioin();
            }

            var worldPoint = this.controlledCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10.0f);
            var id = new Point { x = Mathf.RoundToInt(worldPoint.x), y = Mathf.RoundToInt(worldPoint.y) };

            if (Input.GetMouseButton(0))
            {
                if (!this.cellManager.CellDictionary.ContainsKey(id))
                {
                    this.cellManager.CreateCell(id);
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (this.cellManager.CellDictionary.ContainsKey(id))
                {
                    this.cellManager.RemoveCell(id);
                }
            }
            if (Input.GetMouseButtonDown(2))
            {
                this.dragPosition = worldPoint;
            }
            if (Input.GetMouseButton(2))
            {
                var diff = worldPoint - this.dragPosition;
                this.controlledCamera.transform.position -= diff;
                this.dragPosition = worldPoint;
            }

            for(var i = 0; i < 10; ++i)
            {
                if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha0 + i)))
                {
                    var t = CharFileLoader.Get((char)((int)'0' + i));
                    PresetCell.Apply(t, this.cellManager, id);
                }
            }

            var cameraSize = this.controlledCamera.orthographicSize;
            cameraSize -= Input.mouseScrollDelta.y * 1f;
            cameraSize = Mathf.Max(1, cameraSize);
            this.controlledCamera.orthographicSize = cameraSize;
        }
    }
}
