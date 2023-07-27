using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// manages a single flower with nectar 

public class Flower : MonoBehaviour
{
    [Tooltip("The color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, 0.3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowerColor = new Color(0.5f, 0f, 1f);

    /// the trigger collider representing the nectar 
    [HideInInspector]
    public Collider nectarCollider; 

    /// the solid collider representing the flower petals 
    [HideInInspector]
    private Collider flowerCollider; 

    private Material flowerMaterial;


    /// a vector pointing straight out of the flower
    public Vector3 FlowerUpVector 
    {
        get 
        {
            return nectarCollider.transform.up; 
        }
    }

    public Vector3 FlowerCenterPosition 
    {
        get 
        {
            return nectarCollider.transform.position; 
        }
    }

    public float NectarAmount { get; private set; }

    public bool HasNectar 
    {
        get 
        {
            return NectarAmount > 0f; 
        }
    }

    /// attempt to remove nectar from the flower 
    /// returns the amount of nectar actually removed
    public float Feed(float amount) 
    {
        // track how much is taken 
        float nectarTaken = Mathf.Clamp(amount, 0f, NectarAmount);
        NectarAmount -= nectarTaken;
        if (NectorAmount <= 0) 
        {
            NectarAmount = 0; // no nectar remaining 
            flowerCollider.gameObject.SetActive(false); // hide the flower petals
            nectarCollider.gameObject.SetActive(false); // hide the nectar trigger

            // change the flower color 
            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);
        }

        return nectarTaken; 
    }

    public void ResetFlower()
    {
        NectarAmount = 1f; // reset the nectar amount 
        flowerCollider.gameObject.SetActive(true); // show the flower petals 
        nectarCollider.gameObject.SetActive(true); // show the nectar trigger 

        // change the flower color 
        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);
    }

    // called when flower wakes up  
    private void Awake()
    {
        // find the mesh renderer and get the material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        // find the nectar and flower colliders
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("NectarCollider").GetComponent<Collider>();
    }

}
