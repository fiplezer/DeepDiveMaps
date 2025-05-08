using TMPro;
using UnityEngine;

public class PlayerHedVer : MonoBehaviour
{
    GameObject PlayerObj;

    GameObject Locatie1;
    GameObject Locatie2;
    GameObject Locatie3;
    GameObject Locatie4;

    public GameObject Canvas;
    public TextMeshProUGUI Text;

    private MapReader mapReader;

    private float initialLatitude = 0f;
    private float initialLongitude = 0f;
    private bool isOriginSet = false;

    void Start()
    {
        PlayerObj = GameObject.Find("UserToken");

        Locatie1 = GameObject.Find("Locatie1");
        Locatie2 = GameObject.Find("Locatie2");
        Locatie3 = GameObject.Find("Locatie3");
        Locatie4 = GameObject.Find("Locatie4");

        // Get the MapReader instance
        mapReader = FindObjectOfType<MapReader>();
    }

    void Update()
    {
        // Ensure MapReader is available and GPS is active
        if (mapReader != null && mapReader.gps_ok)
        {
            // Set the origin (0, 0) on the first reading
            if (!isOriginSet)
            {
                initialLatitude = mapReader.Latitude;
                initialLongitude = mapReader.Longitude;
                isOriginSet = true;
            }

            // Calculate relative position in the play space
            float relativeLatitude = mapReader.Latitude - initialLatitude;
            float relativeLongitude = mapReader.Longitude - initialLongitude;

            // Map the relative position to Unity's play space
            Vector3 newPosition = new Vector3(relativeLongitude, PlayerObj.transform.position.y, relativeLatitude);
            PlayerObj.transform.position = newPosition;
        }

        // Check proximity to locations
        if (Vector3.Distance(PlayerObj.transform.position, Locatie1.transform.position) <= 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 1";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie2.transform.position) <= 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 2";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie3.transform.position) <= 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 3";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie4.transform.position) <= 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 4";
        }
        else
        {
            Canvas.SetActive(false);
        }
    }
}
