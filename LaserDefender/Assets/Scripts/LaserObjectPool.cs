using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObjectPool : MonoBehaviour
{
    public static LaserObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject laserPrefab;

     private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(laserPrefab);
            obj.transform.parent= transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    
    public GameObject GetPooledObject()
    {
        for(int i=0; i< pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
