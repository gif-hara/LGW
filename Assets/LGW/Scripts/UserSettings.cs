using HK.Framework.Systems;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class UserSettings
    {
        private const string KeyName = "LGW.UserSettings";

        public static UserSettings Instance
        {
            get
            {
                var instance = SaveData.GetClass<UserSettings>(KeyName, null);
                if(instance == null)
                {
                    instance = new UserSettings();
                    SaveData.SetClass(KeyName, instance);
                    SaveData.Save();
                }

                return instance;
            }
        }

        [SerializeField]
        private int cellSize = 20;
        public int CellSize
        {
            get
            {
                return this.cellSize;
            }
            set
            {
                this.cellSize = value;
                Save();
            }
        }

        private static void Save()
        {
            SaveData.SetClass(KeyName, Instance);
            SaveData.Save();
        }
    }
}
