using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "LGW/CharCell")]
    public sealed class CharCell : ScriptableObject
    {
        [SerializeField]
        private List<Element> elements = new List<Element>();

        public string GetMessage(string key)
        {
            var element = this.elements.Find(e => e.key == key);
            Assert.IsNotNull(element, $"{key}に対応する{typeof(Element).Name}がありませんでした");

            return element.message;
        }

        public string GetMessage(char key)
        {
            return this.GetMessage(key.ToString());
        }

        [Serializable]
        public class Element
        {
            public string key;

            [Multiline]
            public string message;
        }
    }
}
