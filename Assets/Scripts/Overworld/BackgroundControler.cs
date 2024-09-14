using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BackgroundControler : MonoBehaviour
{
    SoundController soundController;

    public Vector2 Displacement;

    public Material BackgroundMaterial;


    [Header("Shader Settings")]
    public Vector2 StartSunPositon;
    public Vector2 EndSunPosition;

    public Gradient SunColor;
    public Gradient SunBloomColor;
    public Gradient CloudColor;
    public Gradient SkyColorLerpFunction;
    public bool noTime;

    [Header("Sound Settings")]
    public Vector2 SeagullSoundRandomRange;
    public float SeagullSoundVolume = 0.25f;
    public float OceanSoundVolume = 0.5f;
    public float WindSoundVolume = 0.5f;

    DayCyleControler dcc;
    // Start is called before the first frame update
    void Start()
    {
        soundController = SoundController.soundController;
        dcc = DayCyleControler.dcc;
        
        if(WorldManager.wm.inOverworld && !WorldManager.wm.inMenu)
        {
            soundController.playOverSound(11, 1);

            StartCoroutine(OceanSounds(0));

            StartCoroutine(WindSounds(0));

            StartCoroutine(SeagullSounds(Random.Range(SeagullSoundRandomRange.x, SeagullSoundRandomRange.y)));
        }
        else if(WorldManager.wm.inMenu)
        {
            StartCoroutine(OceanSounds(0));

            StartCoroutine(WindSounds(0));

            StartCoroutine(SeagullSounds(Random.Range(SeagullSoundRandomRange.x, SeagullSoundRandomRange.y)));
        }
        else
        {
            soundController.playOverSound(11, 1);
        }

    }
    IEnumerator SeagullSounds(float time)
    {
        yield return new WaitForSeconds(time);

        soundController.playOverSound(1, SeagullSoundVolume);
        StartCoroutine(SeagullSounds(Random.Range(SeagullSoundRandomRange.x, SeagullSoundRandomRange.y)));
    }

    IEnumerator OceanSounds(float time)
    {
        yield return new WaitForSeconds(time);

        soundController.playOverSound(0, OceanSoundVolume);
        StartCoroutine(OceanSounds(soundController.sounds[0].length));
    }

    IEnumerator WindSounds(float time)
    {
        yield return new WaitForSeconds(time);

        soundController.playOverSound(2, OceanSoundVolume);
        StartCoroutine(WindSounds(soundController.sounds[2].length));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Camera.main.transform.position.x, 0) + Displacement;

        SetShaderValues();
    }

    void SetShaderValues()
    {
        float time = 0;
        if(!noTime)
        {
            time = dcc.time/dcc.DayLength;
        }
        //Sun Position
        BackgroundMaterial.SetVector("_Sun_Position", Vector2.Lerp(StartSunPositon, EndSunPosition, time));

        //Sun Color
        BackgroundMaterial.SetColor("_Sun_Color", SunColor.Evaluate(time));

        //Bloom Color
        BackgroundMaterial.SetColor("_BloomColor", SunBloomColor.Evaluate(time));

        //Cloud Color
        BackgroundMaterial.SetColor("_Cloud_Color", CloudColor.Evaluate(time));

        //Sky Color
        //Change the sky color gradients in the default values for the night and day sky gradients i nthe shader
        BackgroundMaterial.SetFloat("_Sky_Gradient_Lerp_Function", SkyColorLerpFunction.Evaluate(time).r);

    }
}
