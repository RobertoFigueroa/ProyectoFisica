using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereBehaviour : MonoBehaviour
{
   // public GameObject Panel;
    public Rigidbody rbe; //Rigid Body esfera
    public Rigidbody rbb; //Rigid Body barra
    public Slider velocidad;
    public Slider masaesf;
    public Slider masab;
    //Ignorar los valores iniciales de las siguientes
    private float velocity = 250; //Valor de la velocidad inicial de la esfera
    private float masse = 20; //Valor de la masa de la esfera
    private float massb = 20; //Valor de la masa de la barra
    // Start is called before the first frame update
    public static bool inicio;
    public Toggle typec; 
    private bool typecol; //Tipo de colision
    public GameObject punto_inicial; //Posicion inicial de la barra
    public GameObject punto_final; //Posicion final de la barra
    private Vector3 p_impacto; //Posicion donde impacta la esfera
    private Vector3 l_barra; //Largo de la barra (solo para calculo)
    private float large; //Largo de la barra
    private float cm_barra; //Centro de masa de la barra

    private float angularVelocity;
    private float angularMomentum;


    private bool hasCollided; //For show angular n inertial info.

    void Start()
    {
        /*rbe = transform.GetComponent<Rigidbody>();
        rbb = transform.GetComponent<Rigidbody>();*/
        //Toma los puntos inicial y final de la barra y calcula su largo y su centro de masa
        l_barra = punto_inicial.transform.position - punto_final.transform.position;
        large = l_barra.z;
        cm_barra = large / 2;

        hasCollided = false;
        angularVelocity = 0.0f;
        angularMomentum = 0.0f;


    }

    // Update is called once per frame
    void Update()
    {
        //Inicializa los valores cuando se presione el boton de Inicio
        if (inicio == true)
        {
            Iniciar();
            inicio = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //True = Colision elastica
        hasCollided = true;
        if (typecol == true)
        {
            p_impacto = gameObject.transform.position;
            float vel = rbe.velocity.x;
            rbb.velocity = new Vector3(Vel_B(vel, rbe.mass, rbb.mass, large, cm_barra, p_impacto), 0, 0);
            //rbb.angularVelocity = new Vector3(Vel_Ang_Elastica(vel,rbe.mass, rbb.mass,large,cm_barra,p_impacto),0,0);
            rbb.AddTorque(Vel_Ang_Elastica(vel, rbe.mass, rbb.mass, large, cm_barra, p_impacto), 0, 0, ForceMode.Impulse);
            this.angularVelocity = Vel_Ang_Elastica(vel, rbe.mass, rbb.mass, large, cm_barra, p_impacto);
            rbe.velocity = new Vector3(Vel_final_esf(vel, rbe.mass, rbb.mass, large, cm_barra, p_impacto), 0, 0);
        }
        //False = Colision Inelastica 
        if (typecol == false)
        {
            HingeJoint join = gameObject.AddComponent<HingeJoint>();
            join.connectedBody = collision.rigidbody;
            p_impacto =gameObject.transform.position;
            float veli = rbe.velocity.x;
            rbb.velocity = new Vector3(Vel_CM(veli,rbe.mass, rbb.mass),0,0);
            this.angularVelocity = Vel_Ang_Inelastica(veli, rbe.mass, rbb.mass, cm_barra, p_impacto, large);
            // rbb.angularVelocity = new Vector3(Vel_Ang_Inelastica(veli,rbe.mass, rbb.mass,cm_barra,p_impacto,large),0,0);
            rbb.AddTorque(Vel_Ang_Inelastica(veli, rbe.mass, rbb.mass, cm_barra, p_impacto, large), 0, 0, ForceMode.Impulse);
        }
    }
    
    //Valores con los que inicia la simulacion
    void Iniciar()
    {
        masse = masaesf.value;
        massb = masab.value;
        velocity = velocidad.value;
        typecol = typec.isOn;
        if (rbe && rbb)
        {
            rbe.mass = masse;
            rbb.mass = massb;
            rbe.AddForce(velocity, 0, 0, ForceMode.Impulse);
        }
    }
    /*
     -ve_in=velocidad inicial de la esfera
     -masa1=masa de la esfera
     -masa2=masa de la barra
     -cm_actual=centro de masa actual
     -dis=posicion en la que golpea la esfera
     -largo=largo de la barra
     */

    //Velocidad Angular para colisiones elasticas
    float Vel_Ang_Elastica(float ve_in, float masa1, float masa2, float largo, float cmactual, Vector3 dis)
    {
        float vel_angular = 0;
        this.angularMomentum = Icm_barra(largo, masa2);
        vel_angular = (-1) * ((Brazo(cmactual, dis) * masa2 * Vel_B(ve_in, masa1, masa2, largo, cmactual, dis))/Icm_barra(largo, masa2));
        return vel_angular;
    }

    //Velocidad Angular para colisiones inelasticas
    float Vel_Ang_Inelastica(float ve_in, float masa1, float masa2, float cm_actual, Vector3 dis, float largo)
    {
        float vel_angular = 0;
        this.angularMomentum = Icm_total(cm_actual, dis, largo, masa1, masa2);
        vel_angular = (Brazo(cm_actual, dis) * ve_in * masa1) / Icm_total(cm_actual, dis, largo, masa1, masa2);
        return vel_angular;
    }

    //Velocidad del centro de masa (barra+esfera)
    float Vel_CM(float ve_in, float masa1, float masa2)
    {
        float ve_cm = 0;
        ve_cm = (masa1 * ve_in) / (masa1 + masa2);
        return ve_cm;
    }

    //Velocidad final del centro de masa de la barra en colision elastica
    float Vel_B(float ve_in, float masa1, float masa2, float largo, float cmactual, Vector3 dis)
    {
        float ve_b = 0;
        ve_b = (2 * ve_in) / (1 + (masa2 / masa1) + (((Mathf.Pow(Brazo(cmactual, dis), 2)) * masa2)) / Icm_barra(largo, masa2));
        return ve_b;
    }

    //Nuevo centro de masa (barra+esfera)
    float Nue_CM(float cmactual, float masa1, float masa2, Vector3 dis)
    {
        float n_cm=0;
        n_cm = (masa1 * Brazo(cmactual, dis)) / (masa1 * masa2);
        return n_cm;
    }

    //Momento de Inercia (barra+esfera)
    float Icm_total(float cmactual, Vector3 dis, float largo, float masa1, float masa2)
    {
        float Icm = 0;
        Icm = Icm_barra(largo, masa2) + ((masa1*masa2*(Mathf.Pow(Brazo(cmactual, dis),2)))/(masa1+masa2));
        return Icm;
    }

    //Distancia entre el centro de masa original y la posicion donde choca la esfera (solo en "Z")
    float Brazo(float cmactual, Vector3 dis)
    {
        return (cmactual - dis.z);
    }

    //Momento de Inercia de la barra
    float Icm_barra(float largo, float masa2)
    {
        float Icm = 0;
        Icm = ((masa2 * (Mathf.Pow(largo, 2))) / 12);
        return Icm;
    }

    //Velocidad final de la esfera para colisiones elasticas
    float Vel_final_esf(float ve_in, float masa1, float masa2, float largo, float cmactual, Vector3 dis)
    {
        float Vfinal = 0;
        Vfinal = ve_in - ((masa2 / masa1) * Vel_B(ve_in, masa1, masa2, largo, cmactual, dis));
        return Vfinal;
    }

    void OnGUI()
    {
        if (hasCollided)
        {
            string message = "La velocidad angular es de " + -this.angularVelocity + " rad/s";
            message += "\nEl momento de inercia de la \nbarra es " + this.angularMomentum + " kg·m²/s.";

            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height - 70, 300, 150), message);
        }

    }

}

