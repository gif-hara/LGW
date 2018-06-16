using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PresetCell
    {
        private static Dictionary<string, ElementBundle> bundles = new Dictionary<string, ElementBundle>();

        public static void Apply(string text, CellManager manager, Point offset)
        {
            GetBundle(text).Elements.ForEach(r => r.Apply(manager, offset));
        }

        public static void ApplyFromCharCell(CellManager manager, Point offset, string text, CharCell charCell, int space)
        {
            var id = offset;
            for(var i = 0; i < text.Length; ++i)
            {
                var message = charCell.GetMessage(text[i]);
                Apply(message, manager, id);
                id += new Point(GetBundle(message).Max.x + space, 0);
            }
        }

        private static ElementBundle GetBundle(string text)
        {
            ElementBundle result;
            if (!bundles.TryGetValue(text, out result))
            {
                result = new ElementBundle(text);
                bundles.Add(text, result);
            }

            return result;
        }

        public class ElementBundle
        {
            public readonly List<Element> Elements = new List<Element>();

            public Point Max { get; private set; }

            public ElementBundle(string text)
            {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                var separator = "¥n";
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                var separator = System.Environment.NewLine;
#else
                var separator = System.Environment.NewLine;
#endif
                var splitText = text.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                for (var y = 0; y < splitText.Length; y++)
                {
                    var s = splitText[y];
                    for (var x = 0; x < s.Length; ++x)
                    {
                        var c = s[x];
                        var id = new Point(x, -y);
                        if (c == '*')
                        {
                            this.Elements.Add(new Element { Id = id, IsAlive = true });
                        }
                        else if (c == '-')
                        {
                            this.Elements.Add(new Element { Id = id, IsAlive = false });
                        }
                        this.UpdateMax(id);
                    }
                }
            }

            private void UpdateMax(Point max)
            {
                this.Max = new Point(
                    Mathf.Max(this.Max.x, max.x + 1),
                    Mathf.Min(this.Max.y, max.y - 1)
                );
            }
        }

        public class Element
        {
            public Point Id;

            public bool IsAlive;

            public void Apply(CellManager manager, Point offset)
            {
                var targetId = offset + this.Id;
                if(this.IsAlive && !manager.CellDictionary.ContainsKey(targetId))
                {
                    manager.CreateCell(targetId);
                }
                else if(!this.IsAlive && manager.CellDictionary.ContainsKey(targetId))
                {
                    manager.RemoveCell(targetId);
                }
            }
        }
    }
}
