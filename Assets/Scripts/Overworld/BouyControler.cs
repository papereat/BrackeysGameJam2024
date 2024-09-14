using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BouyControler : MonoBehaviour
{
    public Transform rb;

    public float WaterRange;
    public float WaterHeight;
    public float WaterSpeed;

    public float SampleDistance;

    public float time;

    public float tes1;
    public float tes2;

    [SerializeField]
    List<Sine> sines;

    // Start is called before the first frame update
    void Start()
    {


    }

    void Update()
    {
        float point_a = waterHeight(0);
        float point_b = waterHeight(SampleDistance);
        rb.transform.localPosition = new Vector3(rb.transform.localPosition.x, (point_a + point_b) / 2f, rb.transform.localPosition.z);

        rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * GetAngle(new Vector2(0, point_a), new Vector2(SampleDistance, point_b)));


        time += Time.deltaTime * WaterSpeed;
    }

    float GetAngle(Vector2 a, Vector2 b)
    {
        float slope = (b.y - a.y) / (b.x - a.x);

        return MathF.Atan((-slope) / (1));
    }

    // Update is called once per frame
    float waterHeight(float spot)
    {
        float val = 0;

        foreach (var item in sines)
        {
            val += Mathf.Sin(item.Wavelength * (item.displacement + spot + time));
        }
        return val * WaterRange + WaterHeight;
    }
}
[System.Serializable]
struct Sine
{
    public float displacement;
    public float Power;
    public float Wavelength;
}
