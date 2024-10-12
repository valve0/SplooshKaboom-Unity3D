using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TileManager : MonoBehaviour, IDisposable
{
    public int ID;
    public GameObject IconPlaceHolder;

    public Material[] Materials;

    public GameObject Bombs;

    public bool Selectable = true; 

    public Squid Squid;
    private GameObject Object;
    private bool disposedValue;
    private string state;

    // Start is called before the first frame update
    void Start()
    {
        Object = CreateChildPrefab(IconPlaceHolder, gameObject, transform.position.x, transform.position.y, transform.position.z);         
            //Instantiate(IconPlaceHolder, new Vector3(), transform.rotation);
        SetMaterial("SeaStart");
    }


    // Update is called once per frame
    void Update()
    {
    }

    public bool AttackTile()
    {
        if (Squid == null)
        {
            SetMaterial("Miss");
            Selectable = false;
            Bombs.GetComponent<BombManager>().UseBomb();
            return false;
        }
        else 
        {
            SetMaterial("Hit");
            Selectable = false;
            Squid.HitCounter++;
            Bombs.GetComponent<BombManager>().UseBomb();
            return true;
        }
    }

    void SetMaterial(string state)
    {

        this.state = state;
        int materialIndex = 0;

        if (state == "SeaStart")
        {
            materialIndex = 0;
        }
        else if (state == "Miss")
        {
            materialIndex = 1;
        }
        else if (state == "Hit")
        {
            materialIndex = 2;
        }
        else
        {
            string f ="invalid state: "  + state;
            Debug.LogError(f, this);
        }

        Object.GetComponent<Renderer>().material = Materials[materialIndex];
    }

    private GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), parent.transform.rotation);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }

    #region IDisposal


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~TileController()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }    
    #endregion
}
