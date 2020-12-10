using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class cubes : MonoBehaviour
{
    public List<GameObject> cubeLine;
    public int size;
    public bool rotate;
    public int rotType;
    public Vector3 thisSize;
    public bool rotatetrue;

    void Start() {
        size = GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>().size;
        size = (size * size);
        cubeLine = new List<GameObject>();
        thisSize = GetComponent<BoxCollider>().size;
    }
    void Update() //testing thing
    {
        if (rotatetrue)
        {
            rotatetrue = false;
            Rotate();
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("cube")) {
            cubeLine.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("cube")) {
            cubeLine.Remove(col.gameObject);
        }
    }
    public void Rotate()
    {
        if (thisSize.x == thisSize.y)
        {
            rotType = Random.Range(0, 2);
        }
        if (thisSize.y == thisSize.z)
        {
            rotType = Random.Range(2, 4);
        }
        if (thisSize.x == thisSize.z)
        {
            rotType = Random.Range(4, 6);
        }
        for (int i = 0; i < size; i++)
        {
            cubeLine[i].GetComponent<pivots>().rotationDirection(this.transform.position, rotType);
            cubeLine[i].GetComponent<pivots>().Rotate();
        }
    } 
}
