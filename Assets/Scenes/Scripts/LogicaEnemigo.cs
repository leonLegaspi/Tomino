using UnityEngine;
using UnityEngine.AI;
public class LogicaEnemigo : MonoBehaviour
{
    private Animator animatorEnemigo;

    [Header("Logica enemigo")]
    private float rutina;
    private float cronometro;
    private Quaternion angulo;
    private float rotacion;
    public bool atacando;
    [SerializeField]private float speed;

    [SerializeField] private GameObject targetPlayer;

    public RangoEnemigo rango;

    public NavMeshAgent agente;
    public float distanciaAtaque, radioVision;

    [SerializeField] private LayerMask Ground;
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
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > radioVision)
        {
            agente.enabled = false;
            animatorEnemigo.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if(hit.distance < 1f)
                     angulo = Quaternion.Euler(0, 180, 0);
            }

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

            agente.enabled = true;
            agente.SetDestination(targetPlayer.transform.position);

            if(Vector3.Distance(transform.position, targetPlayer.transform.position) > distanciaAtaque && !atacando)
            {
                animatorEnemigo.SetBool("walk", false);
                animatorEnemigo.SetBool("run", true);
            }
            else
            {
                if(atacando)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    animatorEnemigo.SetBool("walk", false);
                    animatorEnemigo.SetBool("ru", false);
                }
            }
        }

        if(atacando)
        {
            agente.enabled = false;
        }
    }
    
    public void Final_Ani()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) > distanciaAtaque + 0.2f)
        {
            animatorEnemigo.SetBool("attack", false);
        }
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
    }
      
}
