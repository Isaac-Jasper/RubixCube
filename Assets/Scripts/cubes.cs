using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class cubes : MonoBehaviour
{
    public List<GameObject> cubeLine;
    public int size;
    public bool rotate;
    public Vector3 rotType;
    public bool rotatetrue;
    public GameObject targetPos;
    public createCube createCube;
    public Vector3 thisSize;
    public GameObject Rooms;
    public bool canGetPos = true;

    IEnumerator Start() {
        Rooms = GameObject.FindGameObjectWithTag("RoomsHolder");
        thisSize = GetComponent<BoxCollider>().size;
        createCube = GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>();
        size = createCube.size;
        size = (size * size);
        cubeLine = new List<GameObject>();

        yield return null;
        
        rotType = FindRotType();
    }
    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("cube"))
            cubeLine.Add(col.gameObject);

        if (col.CompareTag("cubePos") && col.transform.position == transform.position && canGetPos == true) {
            targetPos = col.gameObject;
            canGetPos = false;
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("cube")) {
            cubeLine.Remove(col.gameObject);
        }
    }
    Vector3 FindRotType() {
        Vector3 rotationType;

        if (thisSize.x == 0) {
            rotationType = new Vector3(90, 0, 0);
        }
        else if (thisSize.y == 0) {
            rotationType = new Vector3(0, 90, 0);
        }
        else if (thisSize.z == 0) {
            rotationType = new Vector3(0, 0, 90);
        } else {
            rotationType = new Vector3(0, 0, 0);
        }
        return rotationType;
    }
    public IEnumerator Rotate(int forwards)
    {
        yield return null;
        if (forwards == 1)
            targetPos.transform.Rotate(rotType, Space.Self);
        if (forwards == 2) 
            targetPos.transform.Rotate(-1 * rotType.x, -1 * rotType.y, -1 * rotType.z, Space.Self);

        for (int i = 0; i < cubeLine.Count; i++)
        {
            cubeLine[i].transform.parent = transform;
        }

        while (Quaternion.Angle(transform.localRotation, targetPos.transform.localRotation) >= 1) {
            if (forwards == 1)
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetPos.transform.localRotation, 2f); //probobly should multiply something by time.delta timne to make this consistant across devices
            if (forwards == 2)
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetPos.transform.localRotation, 2f);

            yield return null;
        }

        transform.localRotation = targetPos.transform.localRotation;

        for (int i = 0; i < cubeLine.Count; i++) {
            cubeLine[i].transform.parent = Rooms.transform;
        }
        createCube.canRotate = true;
    } 
}
