using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem impactParticles;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!checkTags(collision.gameObject))
        {
            bulletImpact();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!checkTags(collision.gameObject))
        {
            bulletImpact();
        }
    }


    private bool checkTags(GameObject objectI)
    {
        List<string> checkList = new List<string>() {"PlayerAttack", "EnemyAttack"};  //List Containing objects for bullets to ignore

        string thisTag = gameObject.tag;

        //If isEnemyBullet then add Enemy to ignored list of tags
        if (thisTag == "EnemyAttack") { checkList.Add("Enemy"); }  
        else { checkList.Add("Player"); }  

        foreach (string name in checkList)
        {
            if (objectI.CompareTag(name)) { return true; }
        }

        return false;
    }


    void bulletImpact()
    {
        if (impactParticles != null)
        {
            Destroy(gameObject);
            Instantiate(impactParticles, transform.position, Quaternion.identity);
        }
    }
}
