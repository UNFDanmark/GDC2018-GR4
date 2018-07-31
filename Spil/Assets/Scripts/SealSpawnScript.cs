using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealSpawnScript : MonoBehaviour {

    static float offset = 0;
    static float spawndepth = -1.9f;
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

        if (Input.GetKeyDown(KeyCode.E)) //Test code
        {
            SpawnSeal();
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


            added.transform.position = (RandomLPoint());
            added.GetComponent<SealScript>().SetTarget(RandomRPoint());
        }
        else
        {
            added.transform.position = (RandomRPoint());
            added.GetComponent<SealScript>().SetTarget(RandomLPoint());
        }
        return added;
             
    }

    public GameObject LeftBackIce, LeftForwardIce, RightForwardIce;

    //TODO make segments
    private Segment 
        LeftBackSegment1 =      new Segment(new Vector3(-17.32f, spawndepth, 0.91f), new Vector3(-18.18f, spawndepth, -7.88f)),
        LeftBackSegment2 =      new Segment(new Vector3(-11.43f, spawndepth, -8.43f), new Vector3(-13.16f, spawndepth, -0.43f)),
        LeftMidSegment =        new Segment(new Vector3(-14.47f, spawndepth, -10.07f), new Vector3(-17.66f, spawndepth, -12.97f)),
        LeftForwardSegment1 =   new Segment(new Vector3(-14.38f, spawndepth, -15.69f), new Vector3(-14.38f, spawndepth, -23.74f)),
        LeftForwardSegment2 =   new Segment(new Vector3(-1.18f, spawndepth, -24.11f), new Vector3(-1.46f, spawndepth, -19.15f));

    private Segment
        RightSegmentA = new Segment(new Vector3(25.91f, spawndepth, 0.77f), new Vector3(24.96f, spawndepth, -1.32f)),
        RightSegmentB = new Segment(new Vector3(24.96f, spawndepth, -1.32f), new Vector3(27.71f, spawndepth, -3.69f)),
        RightSegmentC = new Segment(new Vector3(27.71f, spawndepth, -3.69f), new Vector3(22.96f, spawndepth, -9.95f)),
        RightSegmentD = new Segment(new Vector3(22.96f, spawndepth, -9.95f), new Vector3(27.8f, spawndepth, -17.45f)),
        RightSegmentE = new Segment(new Vector3(27.8f, spawndepth, -17.45f), new Vector3(25.81f, spawndepth, -23.9f));

    private bool IsIceGone(GameObject ice)
    {
        return ice.GetComponent<IceScript>().state == IceScript.STATE.DEAD;
    }

    private Vector3 RandomLPoint()
    {
        return RandomPoint(RandomSegment(LSegments()));
    }
    private Vector3 RandomRPoint()
    {
        return RandomPoint(RandomSegment(RSegments()));
    }

    private Segment[] LSegments()
    {
        return new Segment[] { IsIceGone(LeftBackIce) ? LeftBackSegment2 : LeftBackSegment1, LeftMidSegment, IsIceGone(LeftForwardIce) ? LeftForwardSegment2 : LeftForwardSegment1 };
    }

    private Segment[] RSegments()
    {
        return new Segment[] { RightSegmentA, RightSegmentB, RightSegmentC, RightSegmentD, RightSegmentE };
    }

    //returns random point on the segments.
    public Segment RandomSegment(Segment[] ss)
    {
        float l = 0;
        foreach(Segment s in ss)
        {
            l += s.Length();
        }
        float t = Random.Range(0, l);
        float d = 0;
        foreach (Segment s in ss)
        {
            d += s.Length();
            if (t <= d) return s;
        }
        return ss[ss.Length - 1];

    }

    private Vector3 RandomPoint(Segment s)
    {
        return s.a + s.dist * Random.Range(0f, 1f);
    }

    public class Segment
    {
        public Vector3 a, b, dist;
        public Segment(Vector3 a, Vector3 b)
        {
            this.a = a;
            this.b = b;
            dist = b - a;
        }
        public float Length()
        {
            return dist.magnitude;
        }
    }
}
