using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Zelle : MonoBehaviour, IPointerClickHandler
{
    public Button button;
    public Text minenNahText;
    public Image mine;
    public Image flag;
    public Image zelle;


    public bool isMine = false;
    public bool isMineNahe = false;
    public int mineNahe = 0;
    public Vector2 pos;
    public bool isaufgedeckt;
    public bool isflage;

    int[] xnachbar = { 1, 1, 0, -1, -1, -1, 0, 1 };
    int[] ynachbar = { 0, -1, -1, -1, 0, 1, 1, 1 };

    public AudioClip explosionssound;
    public AudioClip opensound;
    public AudioSource AudioSource;

    void Start()
    {
        minenNahText.GetComponent<Text>();
        mine.GetComponent<Image>();
        flag.GetComponent<Image>();

        minenNahText.gameObject.SetActive(false);
        mine.gameObject.SetActive(false);
        flag.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isaufgedeckt)
        {
            if (!GameObject.Find("Feld").GetComponent<Feld>().Gewonnen && !GameObject.Find("Feld").GetComponent<Feld>().verloren)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    Aufdecken();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    FlagePlaziren();
                }
            }
        }
    }

    public void Aufdecken()
    {        
        isaufgedeckt = true;

        if (isflage)
        {
            FlagePlaziren();
        }
        
        if (GameObject.Find("Feld").GetComponent<Feld>().ersterzug)
        {
            Debug.Log("ersterzug");
            GameObject.Find("Feld").GetComponent<Feld>().Minenplaziren(pos);
        }

        MinenNaheZählen();
        if (isMine)
        {
            AudioSource.clip = explosionssound;
            mine.gameObject.SetActive(true);
            if (!GameObject.Find("Feld").GetComponent<Feld>().verloren)
            {
                StartCoroutine(GameObject.Find("Feld").GetComponent<Feld>().Minenaufdecken());
            }
        }
        else if (isMineNahe)
        {
            AudioSource.clip = opensound;
            Zahlenfärben();
            minenNahText.text = mineNahe.ToString();
            minenNahText.gameObject.SetActive(true);
            GameObject.Find("Feld").GetComponent<Feld>().Siegcheck();
        }
        else
        {
            AudioSource.clip = opensound;
            Nachbarnaufdecken();
            GameObject.Find("Feld").GetComponent<Feld>().Siegcheck();
        }

        AudioSource.Play();
        gameObject.GetComponent<Image>().color = Color.white;
        button.interactable = false;
    }

    void FlagePlaziren()
    {
        isflage = !isflage;
        flag.gameObject.SetActive(isflage);
        if (isflage)
        {
            GameObject.Find("Feld").GetComponent<Feld>().zufindeneminen--;
            PlayerPrefs.SetInt("PlatzierteFlagen", PlayerPrefs.GetInt("PlatzierteFlagen") + 1);

        }
        else
        {
            GameObject.Find("Feld").GetComponent<Feld>().zufindeneminen++;
            PlayerPrefs.SetInt("PlatzierteFlagen", PlayerPrefs.GetInt("PlatzierteFlagen") - 1);

        }
    }

    public void MinenNaheZählen()
    {
        for (int i = 0; i < xnachbar.Length; i++)
        {
            try
            {
                GameObject nachbarzelle = GameObject.Find("Feld").GetComponent<Feld>().spielfeld[xnachbar[i] + (int)pos.x, ynachbar[i] + (int)pos.y];
                if (nachbarzelle.GetComponent<Zelle>().isMine)
                {
                    isMineNahe = true;
                    mineNahe += 1;
                    Zahlenfärben();
                }
            }
            catch { }
        }
    }

    public void Nachbarnaufdecken()
    {
        for (int i = 0; i < xnachbar.Length; i++)
        {
            try
            {
                GameObject nachbarzelle = GameObject.Find("Feld").GetComponent<Feld>().spielfeld[xnachbar[i] + (int)pos.x, ynachbar[i] + (int)pos.y];
                if (!nachbarzelle.GetComponent<Zelle>().isaufgedeckt)
                {
                    nachbarzelle.GetComponent<Zelle>().Aufdecken();
                }
            }
            catch { }
        }
    }

    void Zahlenfärben()
    {
        switch (mineNahe)
        {
            case 1:
                minenNahText.color = Color.blue;
                break;
            case 2:
                minenNahText.color = Color.green;
                break;
            case 3:
                minenNahText.color = Color.red;
                break;
            case 4:
                minenNahText.color = new Color32(128, 0, 128, 255);
                break;
            case 5:
                minenNahText.color = Color.black;
                break;
            case 6:
                minenNahText.color = Color.grey;
                break;
            case 7:
                minenNahText.color = new Color32(128, 0, 0, 255);
                break;
            case 8:
                minenNahText.color = new Color32(64, 224, 208, 255);
                break;
        }
    }

    public void Scaling(int scal)
    {
        mine.rectTransform.sizeDelta = new Vector2(scal, scal);
        minenNahText.rectTransform.sizeDelta = new Vector2(scal, scal);
        flag.rectTransform.sizeDelta = new Vector2(scal,scal);
        zelle.rectTransform.sizeDelta = new Vector2(scal, scal);

        minenNahText.fontSize = scal-(scal/5);
    }
}
