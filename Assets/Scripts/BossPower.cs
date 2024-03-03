using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPower : MonoBehaviour
{
    private int numberToSpawn;

    public GameObject goblin;


    void Update()
    {
        if (this.gameObject.GetComponent<Enemie>().isDead && numberToSpawn < 2)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-2, 2), 0, transform.position.y + Random.Range(-2, 2));
            Instantiate(goblin, pos, Quaternion.identity);
            numberToSpawn++;
        }
    }
}
