using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookControler : MonoBehaviour
{
    public float HookLength;
    UnderworldControler player;
    // Start is called before the first frame update
    void Start()
    {
        player = UnderworldControler.player;
    }

    void Update()
    {
        if (player.activeProjectile)
        {
            HookLength -= Time.deltaTime;

            if (HookLength <= 0)
            {
                player.ReturnProjectile();
            }
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        player.ReturnProjectile();

        if (col.gameObject.layer == 8)
        {
            player.HitEnemyProjectile(col.GetComponent<Enemies>());
        }
    }
}
