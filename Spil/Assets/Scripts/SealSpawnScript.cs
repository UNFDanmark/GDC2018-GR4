using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealSpawnScript : MonoBehaviour {

    static float offset = 0;
    static float spawndepth = 1.9f;
    public Vector3 l1 = new Vector3(-15- offset,    spawndepth, 6);
    public Vector3 l2 = new Vector3(-5+ offset,     spawndepth, -18.7f);
    public Vector3 l3 = new Vector3(-15.7f- offset, spawndepth, -18);
    public Vector3 l4 = new Vector3(23+ offset,     spawndepth, 6.75f);

    public GameObject seal;

    private Vector3 leftSpawn, rightSpawn;
    public GameObject currentSeal;
    // Use this for initialization
    void Start()
    {
        leftSpawn = l2 - l1;
        rightSpawn = l3 - l4;

        for(int i = 0; i < 0; i++)
        {
            SpawnSeal();
        }

    }
    private void Update()
    {
        if (currentSeal == null)
        {
            currentSeal = SpawnSeal();
        }
    }
    private Vector3 SpawnPoint()
    {
        return l1 + leftSpawn * Random.Range(0, 1f);
    }

    private Vector3 TargetPoint()
    {
        Vector3 target = l4 + rightSpawn * Random.Range(0, 1f);
        print(target.x + "," + target.y + "," + target.z);
        return target;
    }

    public GameObject SpawnSeal()
    {
        GameObject added = Instantiate(seal);
        if (Random.Range(0, 2) == 0) { 
            added.transform.position = (SpawnPoint());
            added.GetComponent<SealScript>().SetTarget(TargetPoint());
        }
        else
        {
            added.transform.position = (TargetPoint());
            added.GetComponent<SealScript>().SetTarget(SpawnPoint());
        }
        return added;
             
    }
}
