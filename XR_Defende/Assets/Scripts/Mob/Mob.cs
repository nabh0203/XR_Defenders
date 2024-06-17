using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mob : MonoBehaviour
{
    public float hueMin = 0f;
    public float hueMax = 1f;
    public float saturationMin = 0.7f;
    public float saturationMax = 1f;
    public float valueMin = 0.7f;
    public float valueMax = 1f;

    public float arrangRage = 0.5f;
    public float emissionIntensity = 5f;

    public ParticleSystem enviromentParticle;

    private NavMeshAgent agent;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(new Vector3(0f, 2f, 1f));
        agent.speed *= Random.Range(0.8f, 1.5f);

        RandomColor();
    }

    public void RandomColor()
    {
        var color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        var main = enviromentParticle.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color ,color * Random.Range(1f - arrangRage, 1f+ arrangRage));

        var renderer = enviromentParticle.GetComponent<ParticleSystemRenderer>();
        renderer.material.SetColor("_EmissionColor", color * emissionIntensity);
    }
}
