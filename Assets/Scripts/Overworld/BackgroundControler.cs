using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControler : MonoBehaviour
{
    public Vector2 Displacement;

    public Material BackgroundMaterial;


    [Header("Shader Settings")]
    public Vector2 StartSunPositon;
    public Vector2 EndSunPosition;

    public Gradient SunColor;
    public Gradient CloudColor;
    public Gradient SkyColorLerpFunction;

    DayCyleControler dcc;
    // Start is called before the first frame update
    void Start()
    {
        dcc = DayCyleControler.dcc;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Camera.main.transform.position.x, 0) + Displacement;

        SetShaderValues();
    }

    void SetShaderValues()
    {
        //Sun Position
        BackgroundMaterial.SetVector("_Sun_Position", Vector2.Lerp(StartSunPositon, EndSunPosition, dcc.time / dcc.DayLength));

        //Sun Color
        BackgroundMaterial.SetColor("_Sun_Color", SunColor.Evaluate(dcc.time / dcc.DayLength));

        //Cloud Color
        BackgroundMaterial.SetColor("_Cloud_Color", CloudColor.Evaluate(dcc.time / dcc.DayLength));

        //Sky Color
        //Change the sky color gradients in the default values for the night and day sky gradients i nthe shader
        BackgroundMaterial.SetFloat("_Sky_Gradient_Lerp_Function", SkyColorLerpFunction.Evaluate(dcc.time / dcc.DayLength).r);

    }
}
