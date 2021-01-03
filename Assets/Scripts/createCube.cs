using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class createCube : MonoBehaviour
{
    public Transform CenterCube;
    public GameObject cube;
    public GameObject cubePos;
    public GameObject detector;
    public GameObject cubeclone;
    public GameObject Rooms;
    public GameObject Detector;
    public GameObject CubePosParent;
    public GameObject CenterDecider;
    private GameObject detectorClone;
    public List<GameObject> Detectors;
    public List<GameObject> CubePos;
    public float rotatewait = 0.5f;
    public int size;
    public int turns = 5;
    private float odd;
    public bool canRotate = true;
    public bool confirm = false;

    IEnumerator Start() { //all these yield return nulls need to be replaced with checks instead, even waitUntils would be better
        Detectors = new List<GameObject>();
        CubePos = new List<GameObject>();
        CenterDecider = Instantiate(CenterDecider, new Vector3(size / 2, size / 2, size / 2), CenterDecider.transform.rotation); //the vector here should actually be a gameobject used to reference the center of the cube
        yield return null;
        DetectorCreator();
        yield return null;
        cubeCreator();
        yield return null;
        cubeTargetCreator();
        yield return null;

        if (size % 2 == 1) {
            CenterDecider.GetComponent<CenterDecider>().Distribute();
            Debug.Log("distributeed");
        } else {
            confirm = true;
        }
        yield return new WaitUntil(() => confirm);
        StartCoroutine(RandRotate());
    }

    void DetectorCreator() {
        if (size % 2 == 0) {
            odd = 0.5f;
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd, (size / 2) - odd, i), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, size, 0);
            detectorClone.transform.parent = Detector.transform;
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3(i, (size / 2) - odd, (size / 2) - odd), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(0, size, size);
            detectorClone.transform.parent = Detector.transform;
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++)
        {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd, i, (size / 2) - odd), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, 0, size);
            detectorClone.transform.parent = Detector.transform;
            Detectors.Add(detectorClone);
        }
    }
    void cubeCreator() {
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    for (int z = 0; z < size; z++) {
                    cubeclone = Instantiate(cube, new Vector3(CenterCube.position.x + x, CenterCube.position.y + y, CenterCube.position.z + z), cube.transform.rotation);
                    cubeclone.GetComponent<Renderer>().material.color = Random.ColorHSV();
                    cubeclone.transform.parent = Rooms.transform;
                }
                }
            }
    }
    void cubeTargetCreator()
    {
        GameObject cubePosClone;
        if (size % 2 == 0) {
            odd = 0.5f;
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3((size / 2) - odd, (size / 2) - odd, i), detector.transform.rotation);
            cubePosClone.transform.parent = CubePosParent.transform;
            CubePos.Add(cubePosClone);
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3(i, (size / 2) - odd, (size / 2) - odd), detector.transform.rotation);
            cubePosClone.transform.parent = CubePosParent.transform;
            CubePos.Add(cubePosClone);
        }
        for (int i = 0; i < size; i++) {
            cubePosClone = Instantiate(cubePos, new Vector3((size / 2) - odd, i, (size / 2) - odd), detector.transform.rotation);
            cubePosClone.transform.parent = CubePosParent.transform;
            CubePos.Add(cubePosClone);
        }
    }
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
    }
}
