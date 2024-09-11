using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;

    public SpriteRenderer sr;

    public int current_sprite;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = sprites[current_sprite];
    }
}
