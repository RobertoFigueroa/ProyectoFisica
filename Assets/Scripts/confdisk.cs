using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class confdisk : MonoBehaviour
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
        transform.Find("Canvas").Find("Panel").Find("ViAng").Find("printvi").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("ViAng").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Mdisco").Find("printMasa").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Mdisco").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Radio").Find("printRadio").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Radio").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("radio").Find("printradio").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("radio").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("Masaesf").Find("printmasa").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("Masaesf").GetComponent<Slider>().value.ToString();
        transform.Find("Canvas").Find("Panel").Find("altura").Find("printalt").GetComponent<Text>().text = transform.Find("Canvas").Find("Panel").Find("altura").GetComponent<Slider>().value.ToString();

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
