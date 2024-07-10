﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Sprite onMusic, offMusic, onSfx, offSfx, onMusicPressed, offMusicPressed, onSfxPressed, offSfxPressed;
    [SerializeField] private Button music, sfx;
    [SerializeField] private string DATA_KEY;
    [SerializeField] private int level;
    [SerializeField] private string musicName;
    private float time;
    private LevelData _levelData;
     
    [SerializeField] private BoxClassification _boxSO;  //Scriptable Object chua cac box, moi box co type va sprite rieng
    [SerializeField] public List<BoxClass> _boxtypelist = new List<BoxClass>();        //Danh sach cac box khi duoc nhan doi
    public List<ButtonScript> _board = new List<ButtonScript>();     //Danh sach cac button

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
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        UpdateUI();
        //TimeManager.Instance.StartTracking();
        time = 0;

        InitBoard();
        
    }

    void FixedUpdate()
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
    public void pauseButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Load(string name)
    {
        SceneController.Instance.LoadScene(name);
    }
    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayMusic(name);
    }
    public void PlaySfx(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateUI();
    }
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (GameManager.Instance.isMuteMusic())
        {
            music.GetComponent<Image>().sprite = offMusic;
            SpriteState state = music.spriteState;
            state.pressedSprite = offMusicPressed;
            music.spriteState = state;
        }
        else
        {
            music.GetComponent<Image>().sprite = onMusic;
            SpriteState state = music.spriteState;
            state.pressedSprite = onMusicPressed;
            music.spriteState = state;
        }
        if (GameManager.Instance.isMuteSfx())
        {
            sfx.GetComponent<Image>().sprite = offSfx;
            SpriteState state = sfx.spriteState;
            state.pressedSprite = offSfxPressed;
            sfx.spriteState = state;
        }
        else
        {
            sfx.GetComponent<Image>().sprite = onSfx;
            SpriteState state = sfx.spriteState;
            state.pressedSprite = onSfxPressed;
            sfx.spriteState = state;
        }
    }


    #region Select And Compare Box
    private ButtonScript _box1;
    private ButtonScript _box2;

    public void GetClick(ButtonScript button)
    {
        //Chưa chọn đủ
        if (_box1 == null)
        {
            _box1 = button;
            return;
        }
        _box2 = button;

        //Chọn sai
        if (_box2.buttonType.type != _box1.buttonType.type)
        {
            //Đóng box1 và box2 (thêm hàm)
            /*
             
             */

            _box1 = null;
            _box2 = null;
            return;
        }

        //Chọn đúng
        //Cộng điểm
        /*
         GameManager.Instace.
         */
    }
    #endregion Select And Compare Box


    void Shuffle(List<BoxClass> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            BoxClass temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    void InitBoard()
    {
        //Them cac button vao _board
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            _board.Add(objects[i].GetComponent<ButtonScript>());
        }

        //Chuyen box trong scriptable object vao list va random
        for (int i = 0; i < objects.Length/2; i++)
        {
            _boxtypelist.Add(_boxSO.BoxTypeList[i]);
            _boxtypelist.Add(_boxSO.BoxTypeList[i]);     //Nhan doi so box
        }
        Shuffle(_boxtypelist);                           //Random cac box

        //Gan box vao cac button
        for (int i = 0; i < objects.Length; i++)
        {
            _board[i].buttonType = _boxtypelist[i];

            //Tim Sprites la con cua button
            Transform spritesTransform = _board[i].transform.Find("Sprites");
            GameObject child = spritesTransform.gameObject;

            //Tim ButtonFrame la con Sprites
            Transform buttonFrameTransform = child.transform.Find("ButtonFrame");
            GameObject grandchild = buttonFrameTransform.gameObject;
            Image grandchildImage = grandchild.GetComponent<Image>();

            //Thay doi ButtonFrame image thanh sprite tuong ung trong box (hinh anh phia sau button)
            grandchildImage.sprite = _board[i].buttonType.icon;
        }

        
    }

}