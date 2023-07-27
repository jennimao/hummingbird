using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// manages a collection of flower plants and attached flowers 
public class FlowerArea : MonoBehaviour
{
    // the diameter of the area where the agent and flowers can be
    public const float AreaDiameter = 20f;

    // list of all flower plants 
    private List<GameObject> flowerPlants;

    // lookup dictionary for looking up a flower from a nectar collider
    private Dictionary<Collider, Flower> nectarFlowerDictionary;

    // list of all the flowers 
    public List<Flower> Flowers { get; private set; }

    public void resetFlowers() 
    {
        // reset all flowers at different axes
        foreach (GameObject flower in Flowers) 
        {
            float xRotation = UnityEngine.Random.Range(-5f, 5f);
            float yRotation = UnityEngine.Random.Range(-180f, 180f);
            float zRotation = UnityEngine.Random.Range(-5f, 5f);
            flower.transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        // reset all flower
        foreach (Flower flower in Flowers) 
        {
            flower.ResetFlower();
        }
    }

    public Flower getFlowerFromNectar(Collider nectarCollider) 
    {
        // get the flower that corresponds to this nectar collider 
        return nectarFlowerDictionary[nectarCollider];
    }

    public void Awake() 
    {
        // initialize variables 
        flowerPlants = new List<GameObject>();
        nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    private void Start()
    {
        // find all flowers that are children of this GameObject/Transform
        FindChildFlowers(transform);
    }

    private void FindChildFlowers(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            // if the child has a flower component, add it to the list
            if (child.CompareTag("flower_plant"))
            {
                // found a flower plant 
                flowerPlants.Add(child.gameObject);
                FindChildFlowers(child);
            }
            else
            {
                // not a flower plant, look for a flower component 
                Flower flower = child.GetComponent<Flower>();
                if (flower != null)
                {
                    // found a flower 
                    Flowers.Add(flower);
                    // add nectar collider to the dicionary 
                    nectarFlowerDictionary.Add(flower.nectarCollider, flower);
                }
                else
                {
                    // flower component not found 
                    FindChildFlowers(child);
                }
            }
        }
    }
}
