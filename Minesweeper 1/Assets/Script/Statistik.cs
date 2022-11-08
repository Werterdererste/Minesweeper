using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistik : MonoBehaviour
{
    /*
     * Anzahl von spielen
     * Anzahl von Siegen
     * Platzierte Flagen
     * Falschplatzierte flagen
     * anzahl von einfach;Mittel schwer spielensiegen
     */

    public Text Spielegesamt;
    public Text Siegegesamt;

    public Text SiegeEinfach;

    public Text SiegeMittel;

    public Text SiegeSchwer;

    public Text PlatzierteFlagen;
    public Text FalschPlatzierteFlagen;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Spielegesamt.text = "Spiele Gesamt: " + PlayerPrefs.GetInt("Spielegesamt").ToString();
        Siegegesamt.text = "Siege Gesamt: " + PlayerPrefs.GetInt("Siegegesamt").ToString();

        SiegeEinfach.text = "Siege Einfach: " + PlayerPrefs.GetInt("SiegeEinfach").ToString();

        SiegeMittel.text = "Siege Mittel: " + PlayerPrefs.GetInt("SiegeMittel").ToString();

        SiegeSchwer.text = "Siege Schwer: " + PlayerPrefs.GetInt("SiegeSchwer").ToString();

        PlatzierteFlagen.text = "Platzierte Flage: " + PlayerPrefs.GetInt("PlatzierteFlagen").ToString();
        FalschPlatzierteFlagen.text = "Falsche Flage: " + PlayerPrefs.GetInt("FalschPlatzierteFlagen").ToString();
       
    }
}
