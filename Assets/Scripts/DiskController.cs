using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiskController : MonoBehaviour
{
    public Rigidbody rbd;
    public Rigidbody rbesf1;
    public Rigidbody rbesf2;
    public Slider Vang; //Slider de Velocidad angular inicial del disco
    public Slider Mdis; //Slider de Masa del disco
    public Slider Rad; //Slider de Radio del disco
    public Slider radp;//Slider de posicion de las esferitas
    public Slider mesf; //Slider de masa de las esferas
    public Slider alt; //Slider de altura de la que caen las esferas
    private float speed; //Velocidad angular
    private float altura; //altura de la que caen las esferas
    private float Radio; //tamaño del disco
    private float radio; //del centro del dico a la posicion de las esferas
    public static bool iniciar;
    private bool hasCollided = false;
    private float angularVelocity;
    private float angularMomentum;
    private void Start()
    {
        /*rbd = transform.GetComponent<Rigidbody>();
        rbesf1 = transform.GetComponent<Rigidbody>();
        rbesf2 = transform.GetComponent<Rigidbody>();*/
    }
    void Update()
    {
        // Velocidad angular a la que gira 
        transform.Rotate(Vector3.up*speed * Time.deltaTime);
        //Inicializa los valores al presionar el boton de inicio
        if (iniciar == true)
        {
            Comenzar();
            iniciar = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Cuando caen las pelotitas hace los calculos
        hasCollided = true;
        this.angularMomentum = Itot(rbesf1.mass, this.radio, rbd.mass, this.Radio);
        this.angularVelocity = Vel_Ang_Final(rbesf1.mass, rbd.mass, Vesf(altura), this.speed, this.radio, this.Radio);
        this.speed = this.angularVelocity;
    }

    //Valores con los que inicia la simulacion
    void Comenzar()
    {
        rbd.mass = Mdis.value;
        Radio = 2 * Rad.value;
        this.speed = Vang.value;
        this.radio = Calradio(Rad.value, radp.value);
        transform.localScale = new Vector3(Radio, (float)0.1, Radio);
        this.altura = 3+alt.value;
        rbesf1.position = new Vector3((float)0.48, altura, -radio);
        rbesf2.position = new Vector3((float)-0.48, altura, radio);
        rbesf1.mass = mesf.value;
        rbesf2.mass = mesf.value;
    }

    //Calculo del punto en el disco que estaran las esferitas
    float Calradio(float rad1, float rad2)
    {
        float radt = 0;
        radt = rad1 * (rad2 / 100);
        return radt;
    }

    //Calculo de la velocidad de las pelotitas justo antes de caer en el disco
    float Vesf(float altu)
    {
        float vi = 0;
        vi = Mathf.Pow((2 * (altu-3) * (float)9.8), (float)0.5);
        return vi;
    }

    //Calculo de Momento de Inercia del disco
    float Idis(float masad, float radiod)
    {
        float idisco = 0;
        idisco = ((float)0.5 * masad * Mathf.Pow(radiod, 2));
        return idisco;
    }

    //Calculo del momento de inercia total (disco+esferas)
    float Itot(float masa1, float radio1, float masa2, float radio2)
    {
        float itot = 0;
        itot = Idis(masa2, radio2) + (2 * masa1 * Mathf.Pow(radio1, 2));
        return itot;
    }

    //Calculo de la velocidad angular final cuando tiene las pelotitas
    float Vel_Ang_Final(float masa1, float masa2, float vi,float wi,  float radio1, float radio2)
    {
        float vf = 0;
        vf = ((masa1 * vi * radio1) + (Idis(masa2, radio2) * wi)) / Itot(masa1, radio1, masa2, radio2);
        return vf;
    }

    void OnGUI()
    {
        if (hasCollided)
        {
            string message = "La velocidad angular es de " + this.angularVelocity + " rad/s";
            message += "\nEl momento de inercia del \ndisco es " + this.angularMomentum + " kg·m²/s.";

            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 70, 300, 150), message);
        }

    }
}
