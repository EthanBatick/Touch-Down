using UnityEngine;
using System;
using UnityEngine.UIElements.Experimental;

public class GUI_Fuel : MonoBehaviour
{
    public RectTransform bar;
    [Range(0f, 1f)]
    public float value;
    public GameObject rocket12; // your variable (0 → 1)

    void Update()
    {
        value = (float)rocket12.GetComponent<ControlRocket>().fuelLeft;
        bar.localScale = new Vector3(1, Math.Max(0.0f,value), 1);
        //print(value);
    }
}