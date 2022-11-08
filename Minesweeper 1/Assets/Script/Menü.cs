using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menü : MonoBehaviour
{ 
    public int xanzahl = 8;
    public int yanzahl = 16;
    public int minenzahl = 100;

    public Slider x;
    public Slider y;
    public Slider mine;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(100 * 8 , (100 * 8) + 60, false);

        x.GetComponent<Slider>();
        y.GetComponent<Slider>();
        mine.GetComponent<Slider>();

        button.GetComponent<Button>();
        button.onClick.AddListener(Custom);
    }

    void Update()
    {
        xanzahl = (int)x.value;
        yanzahl = (int)y.value;
        minenzahl = (int)mine.value;
    }
    public void Custom()
    {
        Feld.größe = new Vector2(xanzahl, yanzahl);
        Feld.minenAnzahl = minenzahl;
        Feld.scale = 50;
        Screen.SetResolution(xanzahl * 50, (yanzahl * 50) + 60, false);

        SceneManager.LoadScene("Game");
    }
}
