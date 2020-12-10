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
    private GameObject detectorClone;
    public GameObject cubeclone;
    public List<GameObject> Detectors;
    public List<GameObject> CubePos;
    public float rotatewait = 0.5f;
    public int size;
    public int turns = 5;
    private float odd;
    public int rand;

    IEnumerator Start() {
        Detectors = new List<GameObject>();
        CubePos = new List<GameObject>();

        DetectorCreator();
        yield return null;
        cubeCreator();
        cubePosCreator();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RandRotate());
    }

    void DetectorCreator() {
        if (size % 2 == 0) {
            odd = 0.5f;
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd, (size / 2) - odd, i), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, size, 0);
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++) {
            detectorClone = Instantiate(detector, new Vector3(i, (size / 2) - odd, (size / 2) - odd), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(0, size, size);
            Detectors.Add(detectorClone);
        }
        for (int i = 0; i < size; i++)
        {
            detectorClone = Instantiate(detector, new Vector3((size / 2) - odd, i, (size / 2) - odd), detector.transform.rotation);
            detectorClone.GetComponent<BoxCollider>().size = new Vector3(size, 0, size);
            Detectors.Add(detectorClone);
        }
    }
    void cubeCreator() {
            for (int x = 0; x < size; x++) {
                for (int y = 0; y < size; y++) {
                    for (int z = 0; z < size; z++) {
                    cubeclone = Instantiate(cube, new Vector3(CenterCube.position.x + x, CenterCube.position.y + y, CenterCube.position.z + z), cube.transform.rotation);
                    cubeclone.GetComponent<Renderer>().material.color = Random.ColorHSV();
                }
                }
            }
        }
    void cubePosCreator()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    CubePos.Add(Instantiate(cubePos, new Vector3(CenterCube.position.x + x, CenterCube.position.y + y, CenterCube.position.z + z), cube.transform.rotation));
                }
            }
        }
    }
    IEnumerator RandRotate()
    {
         for (int i = 0; i < turns; i++)
        {
            //CubePosStat(false);
            rand = Random.Range(0, Detectors.Count);
            Detectors[rand].GetComponent<cubes>().Rotate();
            yield return new WaitForSeconds(rotatewait);
            //CubePosStat(true);
        }
        yield return null;
    }
    void CubePosStat(bool posOrNot)
    {
            for(int i = 0; i > CubePos.Count; i++)
            {
                CubePos[i].GetComponent<BoxCollider>().enabled = posOrNot;
            }
    }
}
