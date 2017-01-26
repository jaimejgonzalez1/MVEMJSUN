using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GUI_Canvas : MonoBehaviour
{
    [SerializeField]
    float zOffset = .75f;
    [SerializeField]
    float textFadeInDistance = -10;

    [SerializeField]
    GameObject planetPanel;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text relativePositionInSolarSystemText;
    [SerializeField]
    Text uniqueFactText;
    [SerializeField]
    Text lengthOfDayText;
    [SerializeField]
    Text lengthOfYearText;
    [SerializeField]
    Text temperatureText;
    [SerializeField]
    Text atmosphereAndGeologyText;

    GameObject planet;

    Text[] guiTexts;
    Image[] guiImages;


    bool canFade = true;

    void Start()
    {
        guiTexts = GetComponentsInChildren<Text>();
        guiImages = GetComponentsInChildren<Image>();

        planetPanel.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Planet"))
        {
            planet = GameObject.FindGameObjectWithTag("Planet");    // TODO: change this to be a planet assigned to a body

            planetPanel.transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y, planet.transform.position.z - zOffset); // jaIME AND ALEX: this part sets the position of the UI text
            //planetPanel.transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -Camera.main.transform.position.z));

            planetPanel.SetActive(planet.transform.position.z < textFadeInDistance ? true : false);
            if (canFade) StartCoroutine(FadeGUI(planetPanel.activeInHierarchy ? true : false));
        }
    }

    IEnumerator FadeGUI(bool fadeIn)    // TODO: doesn't work
    {
        canFade = false;

        foreach (Text t in guiTexts) t.color = fadeIn ? Color.clear : Color.white;
        foreach (Image i in guiImages) i.color = fadeIn ? Color.clear : Color.white;

        float timer = 3;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            foreach (Text t in guiTexts) t.color = Color.Lerp(t.color, fadeIn ? Color.clear : Color.white, elapsedTime / timer);
            foreach (Image i in guiImages) i.color = Color.Lerp(i.color, fadeIn ? Color.clear : Color.white, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        canFade = true;
    }
}

