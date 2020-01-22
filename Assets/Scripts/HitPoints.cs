using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitPoints : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxHitPoints;
    public float regeneration;

    [Header("References")]
    public GameObject player;
    public Rigidbody rigidbody;
    public Image healthBar;
    public Text died;
    public Text reason;
    public Text score;

    [Header ("Code")]
    public float currentHitpoints;
    float currentDamage;
    float countdown;
    bool regen;
    bool dead;
    string deadReason;
    string scoreText;
    private float startTime;
    private int t;

    // Sākumā uzstāda Dzīvības, un izsauc metodes, kas ragdoll padara neaktīvu un uzsāk punkti skaitīšanu
    void Start()
    {
        startTime = Time.time;
        dead = false;
        currentHitpoints = maxHitPoints;
        currentDamage = 0;
        regeneration = 1;
        countdown = 5;
        setRigidBodyState(true);
        setColliderState(false);
    }

    //Ragdoll metode Rigidbodies daļas padarīt pretējas animatora modelim --> atradu Youtube, nācās pielabot
    void setRigidBodyState (bool state)
    {
        Rigidbody[] players = GetComponentsInChildren<Rigidbody>();

            foreach(Rigidbody player in players)
        {
            player.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;

    }
    //Ragdoll metode Collider daļas padarīt pretējas animatora modelim --> atradu Youtube, nācās pielabot
    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        for (int x = 0; x != 2; x++)
        {
            colliders[x].enabled = !state;
        }
    }

    // Skaita punktus un reģenerē dzīvības kāmēr !dead
    void Update()
    {
        if (!dead)
        {
            t = Mathf.RoundToInt(Time.time - startTime)*Mathf.RoundToInt(10000 / maxHitPoints / regeneration);
            scoreText = t.ToString();
            score.text = scoreText;
        }
        countdown -= 1;
        regen = countdown < 0 & maxHitPoints > currentHitpoints & currentHitpoints >= 0;
        if (regen)
        {
            currentHitpoints += regeneration;
            healthBar.fillAmount = currentHitpoints / maxHitPoints;
            countdown = 5;
        }
    }

    
    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.CompareTag("Brick"))
        // Ja ieskrien sienā, vai uzkrīt virsū ķieģelis atņem dzīvības skatoties pēc ātruma un masas -->
        // baigi optimizēju lai jēdzīgi sanāk, jāatzīst nav līdz galam kā gribu, ķieģeli krītot nodara mazāk
        // vai tikpat cik vnk ieskrienot sienā, no 20 vienību augstuma krītošs noņem 500-1000 HP, kas nav pietiekami
        // bet ja lieku vairāk ieskrienot sienā nomirs
        {
            currentDamage = (theCollision.relativeVelocity.magnitude/10) * theCollision.rigidbody.mass;

            if(Mathf.Pow(theCollision.relativeVelocity.magnitude, 2)-200>=0)
                // lecot damage
            {
                currentDamage = Mathf.Pow(theCollision.relativeVelocity.magnitude, 2) - 200;
                GotHurt(currentDamage,"fell");
            }
        }

        if (theCollision.gameObject.CompareTag("Projectile"))
            // ja trāpa ložmetējs noņem dzīvības, ja lielgabals, pēc šīs formulas uzreiz var teikt ka beigas
        {
            currentDamage = theCollision.relativeVelocity.magnitude * 50 * theCollision.rigidbody.mass;
        }

        if (theCollision.gameObject.name == "Plane")
            // ja izlec ārā pa logu, vairāk damage nekā ja nokrīt telpās
        {
            currentDamage = Mathf.Pow(theCollision.relativeVelocity.magnitude, 2)*5;
            GotHurt(currentDamage, "Fell");
        }
    }

    private void OnTriggerEnter(Collider theCollider)
    {
        // ja kāds no mērķiem ieiet damagable area trigeri (kas ir atsevišķi no collider)
        // OnCollisionEnter aprēķina damage vislaik, bet šis tikai ja ieiet damagable collider
        if (theCollider.gameObject.CompareTag("Brick"))
        {
            GotHurt(currentDamage,"Physical damage");
        }
        if (theCollider.gameObject.CompareTag("Projectile"))
        {
            GotHurt(currentDamage, "Hit by Projectile");
        }
    }
    // metode, kas atņem dzīvības un ja dzīvības paiet zem nulles, izsauc metodi Die
    public void GotHurt(float x, string y)
    {
        currentHitpoints -= x;
        healthBar.fillAmount = currentHitpoints / maxHitPoints;
        FindObjectOfType<AudioManager>().Play("Hurt");
        Debug.Log(currentHitpoints);
        if (currentHitpoints <= 0)
        {
            Die(y);
        }
    }

    IEnumerator PauseTime(string y)
    {
        died.enabled = true;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3);
        Time.timeScale = 1f;
        reason.enabled = true;
        reason.text = y;
    }
    // die metode, kas aptur rezultāta skaitīšanu, izsauc metodes, lai tēlu padarītu par ragdoll un uzraksta
    // ka miris un "killing blow" avotu
    void Die(string y)
    {
        StartCoroutine(PauseTime(y));
        dead = true;
        GetComponent<Animator>().enabled = false;
        setRigidBodyState(false);
        setColliderState(true);
        GetComponent<AudioSource>().enabled = false;

    }

}
