using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Planet : MonoBehaviour
{
    string planetName;
    string relativePositionInSolarSystem;
    string uniqueFact;
    string lengthOfDay;
    string lengthOfYear;
    string temperature;
    string atmosphereAndGeology;

    public Vector3 position;
    public Vector3 spawnPos;
    private Vector3 sun = Vector3.zero;
    //private Vector3 nAxis = new Vector3(0,0,51.5F);
    public int rotationalSpeed =0;

    float rotationSpeed = 100;

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed));
        transform.RotateAround(sun,Vector3.forward,rotationalSpeed*Time.deltaTime);
    }

    void Start()
    {
        rotationalSpeed = Random.Range(10,30);
        spawnPos = this.transform.position;
    }
 
}

