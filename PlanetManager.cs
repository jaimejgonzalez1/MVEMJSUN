using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class PlanetManager : MonoBehaviour
{
    [SerializeField]
    float planetSpacing = 50;
    [SerializeField]
    float planetDistanceFromCamera = 50;

    [SerializeField]
    BodySourceManager manager;
    [SerializeField]
    GameObject[] planets;

    List<Planet> untetheredPlanets = new List<Planet>();
    IDictionary<ulong, Planet> uniquePlanets = new Dictionary<ulong, Planet>();

    void Start()
    {
      for(int i = 0; i < planets.Length; i++)
        {
            untetheredPlanets.Add(planets[i].GetComponent<Planet>());

            print(untetheredPlanets[i]);
        }
    }

    void Update()
    {

        if (manager.GetData() != null)
        {
            //MovePlanet(manager.GetData()[5]);
            for (int i = 0; i < manager.GetData().Length; i++)
            {
                Body body = manager.GetData()[i];

                if (body.IsTracked)
                {
                    if (uniquePlanets.ContainsKey(body.TrackingId))
                    {
                        MovePlanet(body);
                    } else
                    {
                        StartCoroutine(AssignTrackingID(body.TrackingId));
                    }

                }
            }
        }

        //Here we will check each planet to see if it's corresponding body is still being tracked
        foreach (var planet in uniquePlanets)
        {
            bool anyPlanetIsTracked = false;
            for (int i = 0; i < manager.GetData().Length; i++)
            {
                if (planet.Key == manager.GetData()[i].TrackingId)
                {
                    anyPlanetIsTracked = true;
                }
            }

            if (!anyPlanetIsTracked)
            {
                DestroyPlanet(planet.Key);

            }
        }
    }

    IEnumerator AssignTrackingID(ulong tID)
    {
        yield return new WaitForEndOfFrame();

        uniquePlanets.Add(tID, untetheredPlanets[0]);
        untetheredPlanets.RemoveAt(0);

 
    }

    void DestroyPlanet(ulong untrackedKey)
    {


        // sends planet to be repositioned 
        endPlanetBehavior(untrackedKey);
        untetheredPlanets.Add(uniquePlanets[untrackedKey]);
        uniquePlanets.Remove(untrackedKey);                 // removes the planet from the dictionary once its no longer tracked
    }

    void MovePlanet(Body body)
    {


        uniquePlanets[body.TrackingId].transform.position = new Vector3(body.Joints[JointType.Head].Position.X * -10, 0, -planetDistanceFromCamera);
         //untetheredPlanets[0].transform.position = new Vector3(body.Joints[JointType.SpineMid].Position.X * -10, 0, body.Joints[JointType.SpineMid].Position.Z * -3);
    }
    void endPlanetBehavior(ulong x) {
        // TODO needs to be repositioned at original position of planet
        uniquePlanets[x].transform.position = uniquePlanets[x].spawnPos;

    }
}

