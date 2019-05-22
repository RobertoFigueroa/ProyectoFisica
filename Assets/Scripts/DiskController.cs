using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiskController : MonoBehaviour
{
    public Rigidbody rbd;
    public Rigidbody rbesf1;
    public Rigidbody rbesf2;
    public Slider Vang; //Velocidad angular inicial del disco
    public Slider Mdis; //Masa del disco
    public Slider Rad; //Radio del disco
    public Slider radp;//posicion de las esferitas
    public Slider mesf; //masa de las esferas
    public Slider alt; //altura de la que caen las esferas
    private float speed = 150; //Velocidad angular
    private float altura;
    private float Radio;
    private float radio;
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
        transform.Rotate(Vector3.up*speed * Time.deltaTime);
        if (iniciar == true)
        {
            Comenzar();
            iniciar = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        this.angularMomentum = Itot(rbesf1.mass, this.radio, rbd.mass, this.Radio);
        this.angularVelocity = Vel_Ang_Final(rbesf1.mass, rbd.mass, Vesf(altura), this.speed, this.radio, this.Radio);
        this.speed = this.angularVelocity;
    }
    void Comenzar()
    {
        hasCollided = true;
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

    float Calradio(float rad1, float rad2)
    {
        float radt = 0;
        radt = rad1 * (rad2 / 100);
        return radt;
    }

    float Vesf(float altu)
    {
        float vi = 0;
        vi = Mathf.Pow((2 * (altu-3) * (float)9.8), (float)0.5);
        return vi;
    }

    float Idis(float masad, float radiod)
    {
        float idisco = 0;
        idisco = ((float)0.5 * masad * Mathf.Pow(radiod, 2));
        return idisco;
    }

    float Itot(float masa1, float radio1, float masa2, float radio2)
    {
        float itot = 0;
        itot = Idis(masa2, radio2) + (2 * masa1 * Mathf.Pow(radio1, 2));
        return itot;
    }

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
            message += "\nEl momento de inercia del disco es " + this.angularMomentum + " kg·m²/s.";

            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 70, 300, 150), message);
        }

    }
}
