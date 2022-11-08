using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anzeige : MonoBehaviour
{

    public Text Flage;
    public Text Timer;
    public Image b1, b2;
    public Text t1, t2;
    public Image background;


    public Text win;
    public GameObject Menü;
    public GameObject NewGame;

    int time;

    void Start()
    {
        Flage.GetComponent<Text>();
        Timer.GetComponent<Text>();
        win.GetComponent<Text>();

        win.gameObject.SetActive(false);
        Menü.SetActive(false);
        NewGame.SetActive(false);
    }

    void Update()
    {

        int z = GameObject.Find("Feld").GetComponent<Feld>().zufindeneminen;
        Flage.text = z.ToString();
        if (GameObject.Find("Feld").GetComponent<Feld>().Gewonnen)
        {
            win.text = "Gewonnen";
            win.gameObject.SetActive(true);
            Menü.SetActive(true);
            NewGame.SetActive(true);
        }
        else if (GameObject.Find("Feld").GetComponent<Feld>().verloren)
        {
            win.text = "Verloren";
            win.gameObject.SetActive(true);
            Menü.SetActive(true);
            NewGame.SetActive(true);
        }
        else
        {
            time = (int)Time.timeSinceLevelLoad;
            Timer.text = time.ToString();
        }
    }
    public void UIScaling(int uiscale)
    {
        background.rectTransform.anchoredPosition = new Vector2(0,-uiscale/2);
        background.rectTransform.sizeDelta = new Vector2(800, uiscale);

        b1.rectTransform.anchoredPosition = new Vector2(80, -uiscale / 2);
        b1.rectTransform.sizeDelta = new Vector2(160, uiscale);
        t1.fontSize = uiscale / 2;

        b2.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-80, -uiscale / 2);
        b2.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(160, uiscale);
        t2.fontSize = uiscale / 2;

        Flage.rectTransform.anchoredPosition = new Vector2(-130, -uiscale / 2);
        Flage.fontSize = uiscale / 2;

        Timer.rectTransform.anchoredPosition = new Vector2(130, -uiscale / 2);
        Timer.fontSize = uiscale / 2;
    }
}
