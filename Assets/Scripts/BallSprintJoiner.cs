using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallSprintJoiner : MonoBehaviour
{    
    string layerName = "SphereBase";

    public float SpringForce;

    // Start is called before the first frame update
    void Start()
    {
        AddToChildrenInLayer(transform, layerName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToChildrenInLayer(Transform transform, string layer)
    {
        //przenosze to tablicy 
        var dim = 10;        
        var array = ChildrenToArray(transform, dim, layer);

        for (int i = 0; i<dim; i++)
        {
            for (int j = 0; j<dim; j++)
            {
                //nastepna
                if (i<dim-1)
                    ConnectObjects(array[i,j], array[i+1,j]);

                //po prawej
                if (j<dim-1)
                    ConnectObjects(array[i,j], array[i,j+1]);
            }
        }
    }

    void ConnectObjects(Transform obj1, Transform obj2)
    {
        var comp = obj1.gameObject.AddComponent<SpringJoint>();

        comp.spring = SpringForce;
        comp.connectedBody = obj2.gameObject.GetComponent<Rigidbody>();  
    }

    List<Transform> GetChildren(Transform parent, string layer)
    {
        var res = new List<Transform>();

        foreach (Transform child in parent)
            if (getObjectLayer(child).Equals(layer))
                    res.Add(child);
        
        return res;
    }

    Transform[,] ChildrenToArray(Transform parent, int dim, string layer)
    {
        var res = new Transform[dim,dim];

        var children = GetChildren(parent, layer);

        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                res[i,j] = children[dim*i+j];
            }
        }

        return res;
    }

    int CountChildren(Transform transform, string layer)
    {
        int counter = 0;
        foreach (Transform child in transform)
            if (getObjectLayer(child).Equals(layer))
                counter++;

        return counter;
    }    

    string getObjectLayer(Transform transform) => LayerMask.LayerToName(transform.gameObject.layer);
}
