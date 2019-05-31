using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class config : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        //Muestra la pantalla de Inicio (Panel de UI)
        /*Time.timeScale = 0.0f;
        Panel.gameObject.SetActive(true);*/
    }

    // Update is called once per frame
    void Update()
    {
        //Muestra retroalimentacion de los valores de los Sliders
        transform.Find("Canvas").Find("Panel").Find("Vinicial").Find("printvi").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Vinicial").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Pinicial").Find("printvi").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Pinicial").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Masaesf").Find("printmasa").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Masaesf").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Masabarra").Find("printmasa").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Masabarra").GetComponent<Slider>().value.ToString();
        
        //Para mostrar u ocultar la pantalla de Inicio (Panel de UI) 
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                Panel.gameObject.SetActive(true);
            }
            else if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                Panel.gameObject.SetActive(false);
            }
        }*/
        
    }
}
