using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject PlayerObj;

    GameObject Locatie1;
    GameObject Locatie2;
    GameObject Locatie3;
    GameObject Locatie4;

    public GameObject Canvas;
    public TextMeshProUGUI Text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerObj = GameObject.Find("Player");

        Locatie1 = GameObject.Find("Locatie1");
        Locatie2 = GameObject.Find("Locatie2");
        Locatie3 = GameObject.Find("Locatie3");
        Locatie4 = GameObject.Find("Locatie4");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerObj.transform.position, Locatie1.transform.position) < 5 )
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 1";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie2.transform.position) < 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 2";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie3.transform.position) < 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 3";
        }
        else if (Vector3.Distance(PlayerObj.transform.position, Locatie4.transform.position) < 5)
        {
            Canvas.SetActive(true);
            Text.text = "In de buurt van locatie 4";
        }
        else { Canvas.SetActive(false); }
    }
}
