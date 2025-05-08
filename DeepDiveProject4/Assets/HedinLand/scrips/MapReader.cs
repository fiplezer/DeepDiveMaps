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

    private bool lockGPS = false;

    public float Latitude { get; private set; }
    public float Longitude { get; private set; }

    public float LatitudeStart { get; private set; }
    public float LongitudeStart { get; private set; }

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
        if (gps_ok && !lockGPS)
        {
            // Update from GPS only if lockGPS is false
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            debugText.text
                = "Location: \nLat: " + Latitude
                + " \nLon: " + Longitude
                + " \nAcc: " + Input.location.lastData.horizontalAccuracy;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Lock GPS updates
            lockGPS = true;

            debugText.text = "Stored Location: \nLat: " + Latitude + "\nLon: " + Longitude;

            Debug.Log("Stored Location: Lat: " + Latitude + ", Lon: " + Longitude);
        }

        if (lockGPS)
        {
            // WASD movement
            if (Input.GetKey(KeyCode.W))
            {
                Latitude += 0.01f; // Move north
            }
            if (Input.GetKey(KeyCode.S))
            {
                Latitude -= 0.01f; // Move south
            }
            if (Input.GetKey(KeyCode.A))
            {
                Longitude -= 0.01f; // Move west
            }
            if (Input.GetKey(KeyCode.D))
            {
                Longitude += 0.01f; // Move east
            }

            // Update debug text with new position
            debugText.text = "Stored Location: \nLat: " + Latitude + "\nLon: " + Longitude;

            Debug.Log("Updated Location: Lat: " + Latitude + ", Lon: " + Longitude);
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