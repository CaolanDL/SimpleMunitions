using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem impactParticles;

    #region Impacts
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
    #endregion

    //Check object tags and then decide what to do with this bullet
    private bool checkTags(GameObject objectI)
    {
        List<string> checkList = new List<string>() {"PlayerAttack", "EnemyAttack"};  //List Containing objects for bullets to ignore

        string thisTag = gameObject.tag;

        //If isEnemyBullet then add Enemy to ignored list of tags
        if (thisTag == "EnemyAttack") { checkList.Add("Enemy"); }  
        else { checkList.Add("Player"); }  

        //Check if impactor tags apply
        foreach (string name in checkList)
        {
            if (objectI.CompareTag(name)) { return true; }
        }

        return false;
    }

    //Play particles then destroy object
    void bulletImpact()
    {
        if (impactParticles != null)
        {
            Instantiate(impactParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
