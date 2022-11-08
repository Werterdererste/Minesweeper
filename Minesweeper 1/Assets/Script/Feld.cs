using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feld : MonoBehaviour
{
    public static Vector2 größe = new Vector2(8, 8);
    public static int minenAnzahl = 10;
    public static int scale = 50;
    public static int uiscaling = 60;

    public GameObject[,] spielfeld;
    public GameObject[] minen;
    public GameObject zelle;

    public bool Gewonnen;
    public bool verloren;

    public int zufindeneminen = 0;
    public bool ersterzug = true;
    public GameObject[] MinenichtPlatzieren = new GameObject[9];

    public static int schwirigkeitsgrad = 1;

    void Start()
    {
        PlayerPrefs.SetInt("Spielegesamt", PlayerPrefs.GetInt("Spielegesamt")+1);
        zufindeneminen = minenAnzahl;
        Screen.SetResolution(scale * (int)größe.x,( scale * (int)größe.y) + 60,false);
        spielfeld = new GameObject[(int)größe.x, (int)größe.y];
        minen = new GameObject[minenAnzahl];
        GameObject.Find("anzeige").GetComponent<Anzeige>().UIScaling(uiscaling);
        Generire();
    }

    void Generire()
    {
        for (int x = 0; x < größe.x; x++)
        {
            for (int y = 0; y < größe.y; y++)
            { 
                Vector2 position = new Vector2((x+0.5f)*scale,(y+0.5f)*scale);
                GameObject zellen = Instantiate(zelle, position, Quaternion.identity);
                zellen.transform.SetParent(gameObject.transform);
                zellen.GetComponent<Zelle>().pos = new Vector2(x, y);
                zellen.GetComponent<Zelle>().Scaling(scale);
                spielfeld[x, y] = zellen;
            }
        }
    }

    public void Minenplaziren(Vector2 pos)
    {
        ersterzug = false;
        int randomX;
        int randomY;

        Ausschlißen(pos);

        for (int i = 0; i < minenAnzahl; i++)
        {
            do
            {
                randomX = Random.Range(0, (int)größe.x);
                randomY = Random.Range(0, (int)größe.y);

            } while (spielfeld[randomX, randomY].GetComponent<Zelle>().isMine || isIndexofArray(MinenichtPlatzieren, spielfeld[randomX,randomY]) );
            Debug.Log("mine " + i + " " + randomX + " " + randomY);
            spielfeld[randomX, randomY].GetComponent<Zelle>().isMine = true;
            minen[i] = spielfeld[randomX, randomY];
        }
        Debug.Log("Mienenplatzierende");
    }

    void Ausschlißen(Vector2 pos)
    {
        Debug.Log("Ausschlißen");
        int[] xnachbar = { 1, 1, 0, -1, -1, -1, 0, 1,0 };
        int[] ynachbar = { 0, -1, -1, -1, 0, 1, 1, 1,0 };

        for (int i = 0; i < xnachbar.Length; i++)
        {
            try
            {
                GameObject nachbarzelle = spielfeld[xnachbar[i] + (int)pos.x, ynachbar[i] + (int)pos.y];
                MinenichtPlatzieren[i] = nachbarzelle;
                Debug.Log(nachbarzelle);
                Debug.Log(nachbarzelle.GetComponent<Zelle>().pos);
            }
            catch { Debug.LogError("kein nachbar gefunden"); }
            
        }
    }
    bool isIndexofArray(GameObject[] array, GameObject go)
    {
        bool inside = false;
        try{
            for (int i = 0; array.Length > 0; i++)
            {
                if (array[i] == go)
                {
                    inside = true;
                }

            } 
        }catch { }
        return inside;
    }
    public IEnumerator Minenaufdecken()
    {
        Debug.Log("i");
        verloren = true;
        for (int i = 0; i < minen.Length; i++)
        {
            if (!minen[i].GetComponent<Zelle>().isaufgedeckt)
            {
                yield return new WaitForSeconds(0.2f);
                if (!minen[i].GetComponent<Zelle>().isflage)
                {
                    minen[i].GetComponent<Zelle>().Aufdecken();
                }
                else if (minen[i].GetComponent<Zelle>().isflage)
                {
                    minen[i].GetComponent<Zelle>().zelle.color = Color.green;
                }
            }
        }
        StartCoroutine(FlageFalschCheck());
    }

    IEnumerator FlageFalschCheck()
    {
        for (int x = 0; x < spielfeld.GetLength(0); x++)
        {
            for (int y = 0; y < spielfeld.GetLength(1); y++)
            {
                if (spielfeld[x, y].GetComponent<Zelle>().isflage)
                {
                    yield return new WaitForSeconds(0.2f);
                    if (!spielfeld[x, y].GetComponent<Zelle>().isMine)
                    {
                        spielfeld[x, y].GetComponent<Zelle>().zelle.color = Color.magenta;
                        PlayerPrefs.SetInt("FalschPlatzierteFlagen", PlayerPrefs.GetInt("FalschPlatzierteFlagen") + 1);

                    }
                }
            }
        }
    }

    public void Siegcheck()
    {
        int anzahl = (int)größe.x * (int)größe.y;
        int i = 0;
        for (int x = 0; x < spielfeld.GetLength(0); x++)
        {
            for (int y = 0; y < spielfeld.GetLength(1); y++)
            {
                if (spielfeld[x, y].GetComponent<Zelle>().isMine)
                {
                    i++;
                }
                else if (spielfeld[x, y].GetComponent<Zelle>().isaufgedeckt)
                {
                    i++;
                }
            }
        }
        if (i == anzahl)
        {
            Debug.Log("Gewinn");
            Gewonnen= true;
            PlayerPrefs.SetInt("Siegegesamt", PlayerPrefs.GetInt("Siegegesamt") + 1);

            switch (schwirigkeitsgrad)
            {
                case 1:
                    PlayerPrefs.SetInt("SiegeEinfach", PlayerPrefs.GetInt("SiegeEinfach") + 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("SiegeMittel", PlayerPrefs.GetInt("SiegeMittel") + 1);
                    break;
                case 3:
                    PlayerPrefs.SetInt("SiegeSchwer", PlayerPrefs.GetInt("SiegeSchwer") + 1);
                    break;
            }
        }
    }
}