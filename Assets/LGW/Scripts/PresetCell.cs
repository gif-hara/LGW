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
        private static Dictionary<string, List<Element>> elements = new Dictionary<string, List<Element>>();

        public static void Apply(string text, CellManager manager, Point offset)
        {
            List<Element> result;
            if (!elements.TryGetValue(text, out result))
            {
                result = new List<Element>();
                elements.Add(text, result);
                var splitText = text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (var y = 0; y < splitText.Length; y++)
                {
                    var s = splitText[y];
                    for (var x = 0; x < s.Length; ++x)
                    {
                        var c = s[x];
                        if (c == '*')
                        {
                            result.Add(new Element { Id = new Point(x, -y), IsAlive = true });
                        }
                        else if (c == '-')
                        {
                            result.Add(new Element { Id = new Point(x, -y), IsAlive = false });
                        }
                    }
                }
            }

            result.ForEach(r => r.Apply(manager, offset));
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
