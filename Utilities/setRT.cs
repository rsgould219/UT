/*
    Slightly modified version of the RectTransforms scripts made by
    Eldoir at answers.unity.com
*/
using UnityEngine;

public static class setRT{
     public static void SetLeft(this RectTransform rt, float left){
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
     public static void SetRight(this RectTransform rt, float right){
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
 
     public static void SetTop(this RectTransform rt, float top){
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
     public static void SetBottom(this RectTransform rt, float bottom){
         rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static void SetSidesAsZero(this RectTransform rt){
        rt.offsetMin = new Vector2(0.0f, rt.offsetMin.y);
        rt.offsetMax = new Vector2(0.0f, rt.offsetMax.y);
    }
}