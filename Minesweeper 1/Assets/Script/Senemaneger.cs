using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Senemaneger : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Menü()
    {
        SceneManager.LoadScene("Menü");
    }

    public void Einfach()
    {
        Feld.größe = new Vector2(8, 8);
        Feld.minenAnzahl = 10;
        Feld.scale = 100;
        Feld.uiscaling = 60;

        Screen.SetResolution(100 * 8, (100*8) + 60, false);
        Feld.schwirigkeitsgrad = 1;

        SceneManager.LoadScene("Game");
    }
    public void Mittel()
    {
        Feld.größe = new Vector2(16, 16);
        Feld.minenAnzahl = 40;
        Feld.scale = 50;
        Feld.uiscaling = 60;

        Screen.SetResolution(16*50, (16*50) + 60, false);
        Feld.schwirigkeitsgrad = 2;

        SceneManager.LoadScene("Game");
    }
    public void Schwer()
    {
        Feld.größe = new Vector2(30, 16);
        Feld.minenAnzahl = 99;
        Feld.scale = 50;
        Feld.uiscaling = 32;
        Screen.SetResolution(30*50, (16*50) + 60, false);
        Feld.schwirigkeitsgrad = 3;
        SceneManager.LoadScene("Game");
    }
    public void Statistik()
    {
        SceneManager.LoadScene("Statistik");
    }
}