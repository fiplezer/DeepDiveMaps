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

    public float Latitude { get; private set; }
    public float Longitude { get; private set; }

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
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            Debug.Log("location: " + Latitude + " " + Longitude);
            debugText.text
                = "Location: \nLat: " + Latitude
                + " \nLon: " + Longitude
                + " \nAcc: " + Input.location.lastData.horizontalAccuracy;
            gps_ok = true;
        }
    }

    void Update()
    {
        if (gps_ok)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            debugText.text
                = "Location: \nLat: " + Latitude
                + " \nLon: " + Longitude
                + " \nAcc: " + Input.location.lastData.horizontalAccuracy;
        }
    }

    public void StopGPS()
    {
        Input.location.Stop();
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