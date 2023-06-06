using UnityEngine;
public class LogicaEnemigo : MonoBehaviour
{
    private Animator animatorEnemigo;
    [SerializeField] private BoxCollider BoxCollider;

    [Header("Logica enemigo")]
    private float rutina;
    private float cronometro;
    private Quaternion angulo;
    private float rotacion;
    public bool atacando;
    [SerializeField]private float speed;

    [SerializeField] private GameObject targetPlayer;

    public RangoEnemigo rango;
    private void Start()
    {
        animatorEnemigo = GetComponent<Animator>();
        targetPlayer = GameObject.Find("Player");
    }

    private void Update()
    {
       
        ComportamientoEnemigo();
    }

    public void Idle()
    {
        animatorEnemigo.SetBool("idle", true);
    }

    private void ComportamientoEnemigo()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > 5)
        {
            animatorEnemigo.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 4)
            {
               rutina = Random.Range(0, 3);
                cronometro = 0;
            }

            switch(rutina)
            {
                case 0:
                    animatorEnemigo.SetBool("walk",false);                
                    break;

                case 1:
                    rotacion = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, rotacion, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation =  Quaternion.RotateTowards(transform.rotation,angulo, 0.5f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    animatorEnemigo.SetBool("walk", true);
                    break;
            }
        }

        else
        {
            var lookpos = targetPlayer.transform.position - transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);

            if(Vector3.Distance(transform.position, targetPlayer.transform.position) > 1 && !atacando)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animatorEnemigo.SetBool("walk", false);

                animatorEnemigo.SetBool("run", true);
                transform.Translate(Vector3.forward * (speed * 1.7f) * Time.deltaTime);

                animatorEnemigo.SetBool("attack", false);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animatorEnemigo.SetBool("walk", false);
                animatorEnemigo.SetBool("run", false);              
            }
        }
    }
   
    public void FinalAtacando()
    {
        animatorEnemigo.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;     
    }
}
