using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public static class CharFileLoader
    {
        private static readonly Dictionary<char, TextAsset> textAssets = new Dictionary<char, TextAsset>();

        public static TextAsset Get(char c)
        {
            TextAsset result;
            if(!textAssets.TryGetValue(c, out result))
            {
                result = Resources.Load<TextAsset>($"{c}");
                textAssets.Add(c, result);
            }

            return result;
        }
    }
}
