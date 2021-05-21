using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevel(int _level)
    {
        SceneManager.LoadScene(_level);
    }
    
    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Salir");
    }
}
