using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Returns child name with provided name.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Transform GetChildWithName(this Transform parent, string name)
    {
        Transform target = null;
        if (parent.name == name) return target;
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            Transform result = GetChildWithName(child, name);
            if (result != null) return result;
        }
        return target;
    }

}
