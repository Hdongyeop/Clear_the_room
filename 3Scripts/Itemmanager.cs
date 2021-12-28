using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data
{
    public bool[] coinBool;
    public bool[] diamondBool;
    public bool thingBool;

    public int curCoin;
    public int maxCoin;
    public int curDia;
    public int maxDia;
}

public class Itemmanager : MonoBehaviour
{
    public int stage;

    public GameObject[] coin;
    public GameObject[] diamond;
    public GameObject thing;
    
    public Image coinImg;
    public Text coinText;
    public Image diaImg;
    public Text diaText;
    
    public Data data;

    private void Start()
    {
        data.maxCoin = coin.Length;
        data.maxDia = diamond.Length;

        if (data.maxCoin == 0)
        {
            coinImg.gameObject.SetActive(false);
            coinText.gameObject.SetActive(false);
        }
        else if (data.maxDia == 0)
        {
            diaImg.gameObject.SetActive(false);
            diaText.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        coinText.text = data.curCoin + " / " + data.maxCoin;
        diaText.text = data.curDia + " / " + data.maxDia;
        
        if (data.curCoin == data.maxCoin)
            coinText.color = Color.green;
        if (data.curDia == data.maxDia)
            diaText.color = Color.green;
    }

    public void DataSave()
    {
        File.WriteAllText(Application.dataPath + "/StageItem" + stage + ".json", JsonUtility.ToJson(data));
    }

    public void DataLoad(int indexStage)
    {
        string loadData = File.ReadAllText(Application.dataPath + "/StageItem" + indexStage + ".json");
        Data mData = JsonUtility.FromJson<Data>(loadData);
        data = mData;
    }

    public void StateSave()
    {
        for (int i = 0; i < coin.Length; i++)
            data.coinBool[i] = coin[i].activeSelf;
        
        for(int i=0;i<diamond.Length;i++)
            data.diamondBool[i] = diamond[i].activeSelf;

        data.thingBool = thing.activeSelf;
    }

    public void StateLoad()
    {
        for (int i = 0; i < coin.Length; i++)
            coin[i].SetActive(data.coinBool[i]);
        
        for(int i=0;i<diamond.Length;i++)
            diamond[i].SetActive(data.diamondBool[i]);
        
        thing.SetActive(data.thingBool);
    }
}
