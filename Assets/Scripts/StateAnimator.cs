using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimator : MonoBehaviour
{

    public string CurrentState;
    public List<string> State_Names;
    public SpriteArray[] State_Sprites;

    public int start_frame_in_anim;

    SpriteRenderer sr;
    WorldManager wm;
    public bool left;
    public Vector3 LeftDisplacement;
    public Vector3 RightDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        wm = WorldManager.wm;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = left ? LeftDisplacement : RightDisplacement;
        sr.sprite = GetCurrentSprite();
    }

    Sprite GetCurrentSprite()
    {
        //Gets frame in animation
        int frame = 0;
        if (State_Sprites[State_Names.IndexOf(CurrentState)].loop)
        {
            frame = (wm.currentFrame - start_frame_in_anim) % State_Sprites[State_Names.IndexOf(CurrentState)].sprites.Length;
        }
        else
        {
            frame = Mathf.Min(wm.currentFrame - start_frame_in_anim, State_Sprites[State_Names.IndexOf(CurrentState)].sprites.Length - 1);
        }


        return State_Sprites[State_Names.IndexOf(CurrentState)].sprites[frame];
    }

    public void SwitchState(string state_name)
    {
        CurrentState = state_name;
        start_frame_in_anim = wm.currentFrame;
    }
}

[System.Serializable]
public class SpriteArray
{
    public Sprite[] sprites;
    public bool loop;
}
