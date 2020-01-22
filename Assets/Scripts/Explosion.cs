using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [Header("Stats")]
    public Rigidbody rigidbody;
    public float force;
    public float explosionForce;
    public float explosionRadius;

    //public float explosionUpward;
    //public float remainSize = 0.2f;
    //public int cubesInRow = 1;

    
    [Header("References")]

    public GameObject explosionEffect;
    
    [Header("Code")]
    int soundClipNumber;
    private GameObject instantiatedObj;
    float countdown = 3f;

    // lādiņa uzstādīšana, esmu nooptimizējis lai ir normāli, bet ja grib, var samainīt no unity (diezgan jautri)
    void Start()
    {
        soundClipNumber = 0;
        if (rigidbody.name == "AP"){
            force = 1250000f;
            explosionForce = 100000f;
            explosionRadius = 3.5f;
            //explosionUpward = 0.4f;
        }
        if (rigidbody.name == "HE"){
            force = 1200000f;
            explosionForce = 100000f;
            explosionRadius = 5.0f;
            //explosionUpward = 0.4f;
        }
        rigidbody.AddRelativeForce(0, force * Time.deltaTime, 0 );
    }

    // lādiņiem ir "derīguma termiņš" --> jo gadās ka izlido cauri un aizlido nekurienē, tad lai tie tiek izdzēsti
    // Gravitāti pievieno pēc 2 sekundēm, lai tālie lādiņi sāk krist (darbojas tikai sāna lādiņiem)
    void Update()
    {
        countdown -= Time.deltaTime;
        if (rigidbody.useGravity==false && countdown < 1f)
        {
            rigidbody.useGravity = true;
        }
        if (countdown < 0f)
        {
            Destroy(this.gameObject);
        }
    }

    // trigeris eksplozijai, ja trāpa brick, noņēmu pārējos, jo šķiet loģiskāk
    private void OnTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.CompareTag("Brick")) {
            explode();
        }
    }

    // eksplosijas kods dažādas skaņas, izveido eksplosijas efektu, kuru novāc pēc 4 sekundēm
    // ar Physics overlapshere atrod visus Rigidbody un tiem iedod explosion force dabūju no Brackeys
    // grenade tutorial
    // Ja trāpa spēlētājam izsauc hitpoints metodi gotHurt. Plus mīnus eksplozija ir letāla vienmēr
    public void explode()
    {
        if (soundClipNumber == 0)
        { 
            FindObjectOfType<AudioManager>().Play("Explosion");
            soundClipNumber += 1;
        }
        else if (soundClipNumber == 1)
        {
            FindObjectOfType<AudioManager>().Play("Explosion2");
            soundClipNumber -= 1;
        }
        instantiatedObj = (GameObject) Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] explosionColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in explosionColliders)
        {
            if (nearbyObject.name!= "Plane") { 
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null & rb.tag != "Cannon")
            {
                FindObjectOfType<AudioManager>().Play("BrickScrape");
                FindObjectOfType<AudioManager>().Play("BrickDebris");
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            if (rb != null & rb.tag == "Player")
            {
                FindObjectOfType<HitPoints>().GotHurt(explosionForce,"Died in Explosion");
            }
            }
        }
        Destroy(gameObject);
        Destroy(instantiatedObj,4);

    }

    // Bija ideja, ka pie damage izdzēš objektu un izveido daudz mazus, bet baigi noslogo visu, un rezultāts īsti
    // nav tā vērts
    //    Vector3 explosionPos = transform.position;

    //    Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

    //    foreach(Collider hit in colliders)
    //    {
    //        Rigidbody rb = hit.GetComponent<Rigidbody>();
    //        if(rb != null)
    //        {
    //            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
    //        }

    //    }
    //}

    //void createPiece(int x, int y, int z)
    //{
    //    GameObject piece;
    //    piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    piece.transform.position = transform.position + new Vector3(remainSize * x, remainSize * y, remainSize * z);
    //    piece.transform.localScale = new Vector3(remainSize, remainSize, remainSize);

    //    piece.AddComponent<Rigidbody>();
    //    piece.GetComponent<Rigidbody>().mass = remainSize;
    //}
}
