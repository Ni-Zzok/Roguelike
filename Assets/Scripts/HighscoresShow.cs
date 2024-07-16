using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresShow : MonoBehaviour
{

    public Text textLvl1;
    public Text textLvl2;
    public int highLvl1;
    public int highLvl2;
    public string highLvl1Text;
    public string highLvl2Text;


    // Start is called before the first frame update
    void Start()
    {
        highLvl1 = PlayerPrefs.GetInt("EnemyKilled");
        highLvl2 = PlayerPrefs.GetInt("EnemyKilled");
        highLvl1Text = highLvl1.ToString();
        highLvl2Text = highLvl2.ToString();
        textLvl1.text = highLvl1Text;
        textLvl2.text = highLvl2Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
