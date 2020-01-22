using UnityEngine;

public class MGFire : MonoBehaviour
{
    [Header("MG Stats")]
    public int magazine=500;
    public int magazineCurrent;
    public float reload;

    [Header("References")]
    public GameObject shotRound;
    public GameObject muzzle;


    [Header("Code")]
    //Vector3 muzzleOffset=new Vector3(0.5f,0f,0f);
    float random;
    int mgShot;
    public float pause = 2;


    // Ložmetēja dati, es viņus mainīju sākumā, optimizējot, tad pašreiz esmu salicis startā viņus.
    void Start()
    {
        random = UnityEngine.Random.Range(0, 0.5f);
        magazineCurrent = magazine;
        reload = 0;
        mgShot = 0;
    }
    // trigeris, kas skatās vai spēlētājs ir tuvumā priekšpusē, tikai tad šauj
    private void OnTriggerStay(Collider theCollision)
    {
        if (theCollision.gameObject.CompareTag("Player"))
        {
            OpenFire();
        }
    }

    private void FixedUpdate()
    {
        if (pause > 0)
        { 
            pause-=0.2f;
        }
    }

    // ložmetēja loģika, jāpārlādē, tam ir divas šaušanas skaņas
    private void OpenFire()
    {
        if (pause < 0) { 
            if (magazineCurrent > 0 && reload == 0)
            {
                if (mgShot == 0) { 
                FindObjectOfType<AudioManager>().Play("MG");
                    mgShot += 1;
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("MG");
                    mgShot -= 1;
                }
                ShootBullet();
            }
            else if (magazineCurrent == 0 & reload == 0)
            {
                reload = 0.5f + random;
            }
            else if (magazineCurrent == 0 && reload > 0)
            {
                reload -= Time.deltaTime;
            }
            else
            {
                magazineCurrent = 500;
                reload = 0;
            }
            pause = 1;
        }
        
    }

    private void ShootBullet()
    {     
        //Instantiate(muzzle, position: transform.position+muzzleOffset, transform.rotation);
        GameObject currentRound = Instantiate(shotRound, transform.position,transform.rotation);
        magazineCurrent -= 1;
    }
}
