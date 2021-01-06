using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class cubes : MonoBehaviour
{
    public List<GameObject> cubeLine;
    public GameObject targetPos;
    public GameObject Rooms;
    public createCube createCube;

    public Vector3 thisSize;
    public Vector3 rotType;

    public int size;
    public float speed;

    public bool rotate;
    public bool rotatetrue;
    public bool canGetPos = true;

    void Start() {
        Rooms = GameObject.FindGameObjectWithTag("RoomsHolder");
        createCube = GameObject.FindGameObjectWithTag("CubeCreator").GetComponent<createCube>();

        speed = createCube.speed;
        thisSize = GetComponent<BoxCollider>().size;
        size = createCube.size;
        size = (size * size);
        cubeLine = new List<GameObject>();

        rotType = FindRotType();
    }
    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("cube"))
            cubeLine.Add(col.gameObject);

        if (col.CompareTag("cubePos") && col.transform.position == transform.position && canGetPos == true) {
            targetPos = col.gameObject;
            canGetPos = false;
        }
    } //adds cubes to the list when they enter and adds cubePos as its targetPos if it has the same position
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("cube")) {
            cubeLine.Remove(col.gameObject);
        }
    } //removes the cubes from the list when they exit the trigger
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
    } //determines the rotation type based on the size of its collider, returns a vector3 with its rotation
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
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetPos.transform.localRotation, speed * Time.deltaTime) ;
            if (forwards == 2)
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetPos.transform.localRotation, speed * Time.deltaTime);

            yield return null;
        }

        transform.localRotation = targetPos.transform.localRotation;

        for (int i = 0; i < cubeLine.Count; i++) {
            cubeLine[i].transform.parent = Rooms.transform;
        }
        createCube.canRotate = true;
    } //rotates the collider and makes all the cubes connected children of it, and rotates the Target pos so we can rotate towards it
}
