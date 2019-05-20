using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject Panel;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //Metodo que llama el Boton de Iniciar
    public void Iniciar()
    {
        //Desactiva la pantalla de Inicio (Panel de UI)
        Panel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        //Modifica el valor para iniciar la simulacion en SphereBehaviour.cs
        SphereBehaviour.inicio = true;
    }
    public void Reinicio(string NewScene)
    {
        SceneManager.LoadScene(NewScene);
    }
}
