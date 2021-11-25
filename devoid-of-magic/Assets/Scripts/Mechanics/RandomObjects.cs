using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjects : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] prefabs;
    [SerializeField]
    GameObject[] lights;

    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++) //Add all transforms from "spawnPoins" array to "transforms" List
        {
            transforms.Add(spawnPoints[i]);
        }

        for (int i = 0; i < prefabs.Length; i++) // Add all prefabs to objects List
        {
            objects.Add(prefabs[i]);
        }

        for(int i = 0; i < transforms.Count; i++) // Instantiate (with a chance of skipping) random objects @ random transforms positions. 
        {
            int prefabRand = Random.Range(0, objects.Count);
            int transformRand = Random.Range(0, transforms.Count);
            int rand = Random.Range(0, 5); //Skip object & transform?
            if(rand == 0) //Y
            {
                transforms.RemoveAt(transformRand); //Remove randomly choosen transform from a List.
                objects.RemoveAt(prefabRand); //Remove randomly choosen prefab from a List.
            }
            else //N
            {
                var randomObject = Instantiate(objects[prefabRand], transforms[transformRand].position, Quaternion.identity); //Not skipped -> Instantiate!
                randomObject.transform.SetParent(gameObject.transform);
                transforms.RemoveAt(transformRand);
                objects.RemoveAt(prefabRand);
            }     
        }

        for(int i = 0; i < lights.Length; i++)
        {
            int rand = Random.Range(0, 10);
            if(rand < 3)
            {
                lights[i].SetActive(false);
            }
        }
    }
}
