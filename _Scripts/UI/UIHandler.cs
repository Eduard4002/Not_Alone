using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    public EnemyUnitBase eUB;
    public Slider healthBarSlider;

    

    public GameObject[] panels;
    GameManager mainManager;
    public CameraFollows cameraFollow;

    [Header("SettingsPanel")]
    public Slider sfxSlider;
    public TextMeshProUGUI sfxText;
    public Slider musicSlider;
    public TextMeshProUGUI musicText;

    [Header("Data section")]
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        DeactivatePanels();
        mainManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        LoadSFXVolume();
        LoadMusicVolume();
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC", 1f);
        SetMusicVolume(musicSlider.value);
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        SetSFXVolume(sfxSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && panels[0].activeInHierarchy == false){
            SetPauseMenu(true);
            mainManager.ChangeState(GameState.Paused);
        }
    }
    public void SetPauseMenu(bool activation){
        panels[0].SetActive(activation);
        if(activation == false) mainManager.ChangeState(GameState.Paused);
    }
    public void SetSettingsMenu(bool activation){
        panels[0].SetActive(!activation);
        panels[1].SetActive(activation);
    }
    public void SetGameOverMenu(bool activation){
        panels[0].SetActive(false);
        panels[2].SetActive(activation);
        mainManager.ChangeState(GameState.Paused);
        SetDataSection();
    }
    public void SetWonMenu(bool activation){
        panels[0].SetActive(false);
        panels[3].SetActive(activation);
        mainManager.ChangeState(GameState.Paused);
        SetDataSection();
    }
    public void SetDataSection(){
        enemiesKilled.text = "Enemies killed: " + mainManager.GetEnemiesKilled();
        timerText.text = "Survived: " + mainManager.GetSurvivedTime();
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void SetSFXVolume(float volume){
        mainManager.SetSFXVolume(volume);
        sfxText.text = ""+(int)(volume * 100f);
        PlayerPrefs.SetFloat("SFX", volume);
    }
    public void SetFullscreen(bool fullscreen){
        Screen.fullScreen = fullscreen;
    }
    public void SetMusicVolume(float volume){
        cameraFollow.ChangeMusicVolume(volume);
        musicText.text = ""+(int)(volume * 100f);
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    #region Player
    public void SetHealth(int value){
        healthBarSlider.value = value;
    }

    public void SetMaxHealth(int max){
        healthBarSlider.maxValue = max;
        healthBarSlider.value = max;
    }

    #endregion
    void DeactivatePanels(){
        for(int i = 0; i < panels.Length;i++){
            panels[i].SetActive(false);
        }
    }
}
