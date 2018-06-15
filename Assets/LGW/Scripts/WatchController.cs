using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WatchController : MonoBehaviour
    {
        [SerializeField]
        private float displayWatchSeconds;

        [SerializeField]
        private float nextGenerationDelay;

        [SerializeField]
        private CellManager cellManager;

        [SerializeField]
        private CharCell charCell;

        IEnumerator Start()
        {
            while (true)
            {
                PresetCell.ApplyFromCharCell(this.cellManager, new Point(0, 0), DateTime.Now.ToString(), this.charCell, 1);
                var updateTime = Time.realtimeSinceStartup;
                yield return new WaitForSecondsRealtime(this.displayWatchSeconds);
                while(Time.realtimeSinceStartup - updateTime < 1.0f)
                {
                    this.cellManager.NextGeneratioin();
                    yield return new WaitForSeconds(this.nextGenerationDelay);
                }
            }
        }
    }
}
