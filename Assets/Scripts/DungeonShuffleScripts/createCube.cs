using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCube : MonoBehaviour
{
    /* 
     * This Script Creates the Dungeon and starts the rotating event, 
     * all detectors and rooms are created from here
     */
    public Transform CenterCube;
    private Vector3 centerVector;
    //gameobjects
    public GameObject cube;
    public GameObject cubePos;
    public GameObject detector;
    public GameObject roomsParent;
    public GameObject detectorParent;
    public GameObject cubePosParent;
    public GameObject centerDecider;
    public Transform center;
    //lists
    public List<GameObject> Detectors;
    //ints and floats
    public int size;
    public int turns = 5;
    public float speed = 5f;
    private float odd;
    //private and public bools for simple checks
    public bool canRotate = true;
    public bool confirm = false;
    public bool centerDeciderCreated = false;
    public bool hasDetected = true;
    private bool detectorConfirm = false;
    private bool cubeConfirm = false;
    private bool targetConfirm = false;

    IEnumerator Start() {
        centerVector = center.position;
        Detectors = new List<GameObject>();
        centerDecider = Instantiate(centerDecider, new Vector3((size / 2) + centerVector.x, (size / 2) + centerVector.y, (size / 2) + centerVector.z), centerDecider.transform.rotation); //the vector here should actually be a gameobject used to reference the center of the cube
        yield return new WaitUntil(() => centerDeciderCreated); //these checks stop stuff from happening before it should
        DetectorCreator();
        yield return new WaitUntil(() => detectorConfirm);
        CubeCreator();
        yield return new WaitUntil(() => cubeConfirm);
        CubeTargetCreator();
        if (size % 2 == 1) //if there is a center cube it lets a chekc from the center cube to happen
            hasDetected = false;

        yield return new WaitUntil(() => targetConfirm && hasDetected);

        if (size % 2 == 1) //if there is a center cube it creates the CenterDecider
            centerDecider.GetComponent<CenterDecider>().Distribute();
        else
            confirm = true;

        yield return new WaitUntil(() => confirm);
        StartCoroutine(RandRotate());
    }

    void DetectorCreator() {
        GameObject detectorClone;

        if (size % 2 == 0) {
            odd = 0.5f;
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd + centerVector.x, (size / 2) - odd + centerVector.y, i + centerVector.z), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, size, 0);
            detectorClone.transform.parent = detectorParent.transform;
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3(i + centerVector.x, (size / 2) - odd + centerVector.y, (size / 2) - odd + centerVector.z), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(0, size, size);
            detectorClone.transform.parent = detectorParent.transform;
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++)
        {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd + centerVector.x, i + centerVector.y, (size / 2) - odd + centerVector.z), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, 0, size);
            detectorClone.transform.parent = detectorParent.transform;
            Detectors.Add(detectorClone);
        }
        detectorConfirm = true;
    } //creates detectors and adds them to the list and parent gameobject
    void CubeCreator() {
        GameObject cubeClone;
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    for (int z = 0; z < size; z++) {
                    cubeClone = Instantiate(cube, new Vector3(centerVector.x + x, centerVector.y + y, centerVector.z + z), cube.transform.rotation);
                    cubeClone.GetComponent<Renderer>().material.color = Random.ColorHSV();
                    cubeClone.transform.parent = roomsParent.transform;
                    }
                }
            }
        cubeConfirm = true;
    } //creates all the rooms, adds them to a list, and adds them to the parent Gameobject
    void CubeTargetCreator()
    {
        GameObject cubePosClone;
        if (size % 2 == 0) {
            odd = 0.5f;
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3((size / 2) - odd + centerVector.x, (size / 2) - odd + centerVector.y, i + centerVector.z), detector.transform.rotation);
            cubePosClone.transform.parent = cubePosParent.transform;
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3(i + centerVector.x, (size / 2) - odd + centerVector.y, (size / 2) - odd + centerVector.z), detector.transform.rotation);
            cubePosClone.transform.parent = cubePosParent.transform;
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3((size / 2) - odd + centerVector.x, i + centerVector.y, (size / 2) - odd + centerVector.z), detector.transform.rotation);
            cubePosClone.transform.parent = cubePosParent.transform;
        }
        targetConfirm = true;
    } //creates all the Targets which are used as pivot centers and reference angles and adds them to the parent Gameobject
    IEnumerator RandRotate() {
        int rand;
        int forwards;

        for (int i = 0; i < turns; i++) {
            if (canRotate) {
                canRotate = false;


                rand = Random.Range(0, Detectors.Count);
                forwards = Random.Range(1, 3);
                StartCoroutine(Detectors[rand].GetComponent<cubes>().Rotate(forwards));
            }
            yield return new WaitUntil(() => canRotate);
        }
    } //will roitate the cube as many turns as the turns variable, starts rotate() on the specified Cubes Script
}
