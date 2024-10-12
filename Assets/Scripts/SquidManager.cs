using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SquidManager : MonoBehaviour
{
    public GameObject Prefab;
    public Material[] Materials;

    private GameObject[] squidCollection;
    private Queue<GameObject> queueOfSquids;

    private float distanceBetweenSquids = 1F;

    public void GenerateSquids(int numSquids)
    {
        queueOfSquids = new Queue<GameObject>();
        squidCollection = new GameObject[numSquids];

        for (int i = 0; i < numSquids; i++)
        {
            // create a new skyscraper from prefab selection at right edge of screen
            squidCollection[i] = CreateChildPrefab(Prefab, gameObject,
                transform.position.x,
                transform.position.y - distanceBetweenSquids * i,
                transform.position.z);

            SetMaterial(squidCollection[i], "alive");

            queueOfSquids.Enqueue(squidCollection[i]);
        }
    }

    public void KillSquid()
    {
        if (queueOfSquids.Count == 0)
            return;

        SetMaterial(queueOfSquids.Dequeue(), "dead");
    }

    //call this method with any prefab index and any material index
    void SetMaterial(GameObject squid, string squidState)
    {

        int materialIndex = 0;

        if (squidState == "alive")
        {
            materialIndex = 0;
        }
        else if (squidState == "dead")
        {
            materialIndex = 1;
        }
        else
        {
            Debug.LogError("Invalid state", this);
        }

        squid.GetComponent<Renderer>().material = Materials[materialIndex];
    }

    private GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), parent.transform.rotation);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }

}
