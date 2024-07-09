﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {

        }
        else
        {
            _instance = this;
        }

    }
    #endregion Singleton
    [SerializeField] private string DATA_KEY;
    [SerializeField] private int level;
    [SerializeField] private string musicName;
    private LevelData _levelData;

    void Start()
    {
        AudioManager.Instance.PlayMusic(musicName);
        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            //nếu có

            //JSON hóa từ string đã lưu thành class
            string savedJSON = PlayerPrefs.GetString(DATA_KEY);

            //gán nó cho _gameData
            this._levelData = JsonUtility.FromJson<LevelData>(savedJSON);
        }
        else
        {
            Init();
        }
    }

    void Update()
    {

    }
    public void Init()
    {

        this._levelData = new LevelData();
        SaveData();
    }
    private void SaveData()
    {
        //JSON hóa data clas
        string dataJSON = JsonUtility.ToJson(this._levelData);
        //save JSON string
        PlayerPrefs.SetString(DATA_KEY, dataJSON);
    }
    public int getLv() { return level; }

    #region Select And Compare Box

    private ButtonScript _box1;
    private ButtonScript _box2;

    public void GetClick(ButtonScript button)
    {
        if (_box1 == null)
        {
            _box1 = button;
            return;
        }
        _box2 = button;

        if (_box2.buttonType.type != _box1.buttonType.type)
        {
            //Đóng box1 và box2 (thêm hàm)
            /*
             
             */

            _box1 = null;
            _box2 = null;
            return;
        }

        //Cộng điểm
        /*
         GameManager.Instace.
         */
    }

    #endregion Select And Compare Box
}