using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For casting fake shadows beneath sprite render components
/// </summary>

public class FakeShadowCast : MonoBehaviour
{
    [SerializeField]
    private Material sMaterial;

    private SpriteRenderer rSprite;
    private GameObject shadowObject = null;

    private Vector3 offset = new Vector3(-0.1f, -0.1f, 0);

    // Create an empty child object with same sprite and shadow material
    void Start()
    {
        sMaterial = Resources.Load<Material>("Shadow");

        rSprite = GetComponent<SpriteRenderer>();

        if (rSprite != null)
        {
            shadowObject = new GameObject("FakeShadow");

            SpriteRenderer shadowSprite = shadowObject.AddComponent<SpriteRenderer>();
            shadowSprite.sprite = rSprite.sprite;
            shadowSprite.material = sMaterial;
            shadowSprite.sortingLayerID = rSprite.sortingLayerID;
            shadowSprite.sortingOrder = -1;


            shadowObject.transform.localRotation = transform.rotation;

            shadowObject.transform.SetParent(transform);
            shadowObject.transform.localScale = Vector3.one;
            copyPosition();
        }

    }

    // Set position to shadow offset
    void Update()
    {
        if (shadowObject != null)
        {
            copyPosition();
        }
    }

    void copyPosition()
    {
        shadowObject.transform.position = transform.position + offset;
    }
}