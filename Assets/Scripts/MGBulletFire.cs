using UnityEngine;

public class MGBulletFire : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rigidbody;

    [Header("Code")]
    float force = 10000f;
    float countdown = 8f;

    // Izveidojot objektu, tas uzreiz tiek izšauts
    void Start()
    {
        rigidbody.AddRelativeForce(0, 5, force * Time.deltaTime);
        rigidbody.useGravity = false;
    }

    //Pēc pāris sekundēm ieslēdz gravitāti, lai lode sāk krist, un pēc tam lode tiek aizvākta --> tāpat kā lādiņi
    void FixedUpdate()
    {
        countdown -= Time.deltaTime;
        if (countdown < 1 & countdown > 0)
        {
            rigidbody.useGravity = true;
        }
        else if (countdown < 0)
        {
            Destroy(this.gameObject);
        }
    }
    // ja lode trāpa kaut kam tā tiek aizvākta, ja mājas detaļai, tad pazūd --> Citādi 
    //daudzas atsitās un tad bija dīvaini, ka uzripo Tev lode un Tu dabū mizā.
    void OnCollisionEnter(Collision theCollision)
    {
        Destroy(this.gameObject);
        if (theCollision.gameObject.tag == "Brick")
        {
        FindObjectOfType<AudioManager>().Play("BulletImpact");
        }
    }
}
