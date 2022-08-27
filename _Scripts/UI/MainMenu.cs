using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject[] panels;
    public void StartGame(){
        SceneManager.LoadScene("MainGame");
    }
    public void SetTutorialPanel(bool activation){
        panels[0].SetActive(!activation);
        panels[1].SetActive(activation);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
