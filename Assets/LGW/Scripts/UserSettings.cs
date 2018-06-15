using HK.Framework.Systems;
using System;
using UnityEngine;
using UnityEngine.Assertions;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
        private float cellSize = 100;
        public float CellSize
        {
            get
            {
                return this.cellSize * 0.01f;
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

#if UNITY_EDITOR
        [MenuItem("SaveData/Clear")]
        private static void Clear()
        {
            SaveData.Clear();
        }
#endif
    }
}
