using UnityEngine;
using TMPro;
using System;
using System.Collections;
public class MapReader : MonoBehaviour
{
    [SerializeField]
    private char unit = 'K';

    public TMP_Text debugText;
    public bool gps_ok = false;
    float PI = Mathf.PI;

    GPSloc startLoc = new GPSloc();
    GPSloc currLoc = new GPSloc();

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location is not enabled");
            debugText.text = "Location is not enabled";
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugText.text = "Timed out";
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            debugText.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            Debug.Log("location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
            debugText.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nAlt: " + Input.location.lastData.altitude
                + " \nAcc: " + Input.location.lastData.horizontalAccuracy
                + " \nTime: " + Input.location.lastData.timestamp;
            gps_ok = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gps_ok)
        {
            debugText.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nAcc: " + Input.location.lastData.horizontalAccuracy;
        }
    }
}

public class GPSloc
{
    float lon;
    float lat;

    public GPSloc()
    {
        lon = 0;
        lat = 0;
    }
    public GPSloc(float lon, float lat)
    {
        this.lon = lon;
        this.lat = lat;
    }

    public string getLocData()
    {
        return "Lat: " + lat + " Lon: " + lon;
    }
}