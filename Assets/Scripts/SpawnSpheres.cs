using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpheres : MonoBehaviour
{
    public Transform Seed; //pierwsza kulka w warstwie
    public int BaseDimension = 10;
    public int SpawnedObjectOffset = 1;

    public float HeightOffset = 0f;

    public Transform Parent; //rodzic kulek

    public int NumberOfBallsInLayer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects(Seed, BaseDimension, BaseDimension);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects(Transform seed, int xDimension, int zDimension)
    {
        for (int i = 0; i < xDimension; i++)
            for (int j = 0; j < zDimension; j++)
                SpawnObject(seed, i, j);
    }

    void SpawnObject(Transform seed, int xOffset, int zOffset)
    {
        if (xOffset == zOffset && zOffset == 0) return; //żeby nie utworzyć obiektu w pozycji obiektu wyjściowego (seed)

        var originalPosition = seed.position;
        var newPosition = GetNewPosition(originalPosition, SpawnedObjectOffset, xOffset, zOffset);

        var clone = Instantiate(seed, newPosition, Quaternion.identity);
        clone.name = seed.gameObject.name + "_" + xOffset.ToString() + "-" + zOffset.ToString();
        clone.parent = Parent;

        NumberOfBallsInLayer++;
    }

    Vector3 GetNewPosition(Vector3 originalPosition, int generalOffset, int xOffset, int zOffset)
    {        
        var x = originalPosition.x;
        var y = originalPosition.y;
        var z = originalPosition.z;

        return new Vector3(
            x + generalOffset * xOffset, 
            y + HeightOffset,
            z + generalOffset * zOffset);
    }
}
