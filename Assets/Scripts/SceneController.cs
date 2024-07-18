using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    #region Singleton
    private static SceneController _instance;
    public static SceneController Instance => _instance;

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
    private void Start()
    {
       
    }
    public void LoadScene(string senceName)
    {
        DOTween.KillAll();
        SceneManager.LoadScene(senceName);
    }
    public void LoadNext()
    {
        GameManager.Instance.LevelIndex++;
        SceneController.Instance.LoadScene("GameScene");
    }
}
