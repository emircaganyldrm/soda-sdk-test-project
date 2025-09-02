using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{


    //---------------------------------------------------------------------------------
    public static float ClampAngle(float angle, float min, float max) // To avoid euler angle clamping problems
    {
        float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        return Mathf.Clamp(angle, min + floor, max + floor);
    }


    //---------------------------------------------------------------------------------
    public static void Move<T>(this List<T> list, T item, int newIndex) // To move a list element to another index
    {
        if (item != null)
        {
            var oldIndex = list.IndexOf(item);
            if (oldIndex > -1)
            {
                list.RemoveAt(oldIndex);

                if (newIndex > oldIndex) newIndex--;

                list.Insert(newIndex, item);
            }
        }
    }
}
