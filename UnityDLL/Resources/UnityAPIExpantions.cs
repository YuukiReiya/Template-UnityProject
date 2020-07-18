using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class UnityAPIExpantions
    {
        /// <summary>
        /// Find + TryGetComponent
        /// </summary>
        /// <param name="name">検索するオブジェクトの名前</param>
        /// <param name="component">取得するコンポーネントの参照</param>
        /// <returns>成功判定</returns>
        public static bool FindTryGetComponent<T>(this GameObject self, string name, out T component) where T : Component
        {
            component = null;
            var obj = GameObject.Find(name);
            if (obj == null)
            {
                Debug.LogError("No object named " + "<color=red>\"" + name + "\"</color>" + " was found.", self);
                return false;
            }
            if (!obj.TryGetComponent(out component))
            {
                Debug.LogError("Failed to get component named " + "<color=red>\"" + typeof(T) + "\"</color>.", obj);
                return false;
            }
            return true;
        }
    }
}
