using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeShadowCast : MonoBehaviour
{
    [SerializeField]
    private Material sMaterial;

    private SpriteRenderer rSprite;
    private GameObject shadowObject;

    private Vector3 offset = new Vector3(-0.1f,-0.1f, 0);

    // Create an empty child object with same sprite and shadow material
    void Start()
    {
        sMaterial = Resources.Load<Material>("Shadow");

        rSprite = GetComponent<SpriteRenderer>();

        shadowObject = Instantiate(new GameObject(), transform);

        shadowObject.name = "FakeShadow";

        //shadowObject.transform.localScale = gameObject.transform.localScale;
        SpriteRenderer shadowSprite = shadowObject.AddComponent<SpriteRenderer>();
        shadowSprite.sprite = rSprite.sprite;
        shadowSprite.material = sMaterial;
        shadowSprite.sortingLayerID = rSprite.sortingLayerID;
        shadowSprite.sortingOrder = -1;
        
    }

    // Set position to shadow offset
    void Update()
    {
        shadowObject.transform.position = transform.position + offset;
    }
}
