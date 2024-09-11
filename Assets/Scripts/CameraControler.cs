using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public static CameraControler cameraControler;
    public Vector2 watch;

    public float lerp_scale;
    public float lerp_offset;
    Camera cam;




    void Awake()
    {
        cameraControler = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(watch.x, watch.y, cam.transform.position.z), 1 / (1 + Mathf.Exp(-(lerp_scale * Vector3.Distance(new Vector3(watch.x, watch.y, cam.transform.position.z), cam.transform.position) - lerp_offset))));
    }
}
