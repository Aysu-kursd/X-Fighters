using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    public Collider col;
    public float DamageAmount = 0.1f;

    public bool EmitFX = false;
    public ParticleSystem Particles;
    public string ParticleType = "P21";

    private GameObject ChoosenParticles;

    private void Start()
    {
        ChoosenParticles = GameObject.Find(ParticleType);
        Particles = ChoosenParticles.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player1Actions.Hits == false)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            if(EmitFX == true)
            {
                Particles.Play();
                Time.timeScale = 0.7f;
            }
            Player1Actions.Hits = true;
            SaveScript.Player2Health -= DamageAmount;

            if (SaveScript.Player2Timer < 2.0f)
            {
                SaveScript.Player2Timer += 2.0f;
            }

        }
    }
}
