using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Manager : MonoBehaviour
{
    [Header("ENEMY")]
    public List<Enemy> enemyList;

    [SerializeField]
    private float enemyGenerateTimeLimit;
    private float levelTime;

    [Header("LEVEL START-END")]
    [SerializeField]
    private GameObject levelDisplay;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private int levelNumber = 1;

    [SerializeField]
    private GameObject moneyDisplay;
    [SerializeField]
    private GameObject healthDisplay;
    [SerializeField]
    private GameObject soldierPanel;

    [SerializeField]
    private GameObject destroySoldierButton;

    [SerializeField]
    private GameObject levelEndPanel;
    [SerializeField]
    private GameObject levelFailedText;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject levelCompletedText;
    [SerializeField]
    private GameObject nextButton;

    [Header("ECONOMY")]
    [SerializeField]
    private float money;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    private bool isLevelFinished;

    public static Manager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        levelText.text = "LEVEL - " + levelNumber.ToString();
        StartLevel();
    }

    void Update()
    {
        #region ENEMY
        levelTime += Time.deltaTime;
        if (levelTime > enemyGenerateTimeLimit)
        {
            Debug.Log("Time is done");
            if (enemyList.Count <= 0 && !isLevelFinished)
            {
                Debug.Log("There is no enemy");

                isLevelFinished = true;
                FinishLevel(true);
            }

            EnemyBase.instance.StopGenerate();
        }
        #endregion

        #region ECONOMY
        moneyText.text = money.ToString();
        #endregion
    }

    #region LEVEL START-END
    //FINISH
    public void FinishLevel(bool isLevelCompleted)
    {
        levelDisplay.transform.DOScale(Vector3.zero, 1);
        soldierPanel.transform.DOScale(Vector3.zero, 1);
        moneyDisplay.transform.DOScale(Vector3.zero, 1);
        healthDisplay.transform.DOScale(Vector3.zero, 1);
        destroySoldierButton.transform.DOScale(Vector3.zero, 1);

        levelEndPanel.transform.DOScale(Vector3.one, 1);

        if (isLevelCompleted)
        {
            StartCoroutine(LevelCompletedRoutine());
        }
        else
        {
            StartCoroutine(LevelFailedRoutine());
        }
    }

    private IEnumerator LevelCompletedRoutine()
    {
        levelCompletedText.transform.DOScale(Vector3.one, 1);
        nextButton.transform.DOScale(Vector3.one, 1);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator LevelFailedRoutine()
    {
        levelFailedText.transform.DOScale(Vector3.one, 1);
        restartButton.transform.DOScale(Vector3.one, 1);
        yield return new WaitForSeconds(1);
    }

    //START
    public void StartLevel()
    {
        StartCoroutine(StartLevelRoutine());
    }
    private IEnumerator StartLevelRoutine()
    {
        levelDisplay.transform.DOScale(Vector3.one, 1);
        soldierPanel.transform.DOScale(Vector3.one, 1);
        moneyDisplay.transform.DOScale(Vector3.one, 1);
        healthDisplay.transform.DOScale(Vector3.one, 1);
        destroySoldierButton.transform.DOScale(Vector3.one, 1);
        yield return new WaitForSeconds(1);
        EnemyBase.instance.StartGenerate();
    }
    #endregion

    #region ENEMY CONRTOL
    public void AddEnemy(Enemy _enemy)
    {
        enemyList.Add(_enemy);
    }

    public void RemoveEnemy(Enemy _enemy)
    {
        enemyList.Remove(_enemy);
    }
    #endregion

    #region ECONOMY
    public float GetMoney()
    {
        return money;
    }
    public void IncreaseMoney(float _value)
    {
        money += _value;
    }
    public void DecreaseMoney(float _value)
    {
        money -= _value;
    }
    #endregion

    public void RestartLevel()
    {
        SceneManager.LoadScene(levelNumber - 1);
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings > levelNumber)
            SceneManager.LoadScene(levelNumber);
        else
            SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
