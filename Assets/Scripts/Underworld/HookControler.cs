using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookControler : MonoBehaviour
{
    public float HookLength;
    UnderworldControler player;
    public List<int> IDsHit;
    
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

        if (col.gameObject.layer == 8 && !IDsHit.Contains(col.GetComponent<Enemies>().id))
        {
            IDsHit.Add(col.GetComponent<Enemies>().id);
            
            player.HitEnemyProjectile(col.GetComponent<Enemies>());
            
            if(player.worldManager.Capacity[player.fishingRod.Capacity] >= Random.Range(0.0f, 1.0f))
            {
                return;
            }

            player.ReturnProjectile();
        }       
        else
        {
            player.ReturnProjectile();
        }
    }
}
