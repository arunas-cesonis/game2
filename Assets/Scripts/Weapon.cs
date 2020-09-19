using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            GameManager.getInstance().HitEnemy(collider.gameObject);
        }
    }
}
