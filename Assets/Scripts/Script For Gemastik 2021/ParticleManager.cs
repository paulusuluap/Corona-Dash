using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private List<ParticleSystem> particles = new List<ParticleSystem>();
    private List<ParticleSystem> coronaParticles = new List<ParticleSystem>();

    private void Awake() {
        instance = this;
        
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            ParticleSystem p = transform.GetChild(i).GetComponent<ParticleSystem>();
            particles.Add(p);
        }
    }

    public void IdleParticles (string state)
    {
        switch(state)
        {
            case "PrizeActive":
                if(!transform.GetChild(0).gameObject.activeInHierarchy) 
                    transform.GetChild(0).gameObject.SetActive(true);
                else 
                {
                    particles[0].Simulate(0f, true, true);
                    particles[0].Play();
                }
                break;
            case "PrizeTaken":
                if(!transform.GetChild(2).gameObject.activeInHierarchy) 
                    transform.GetChild(2).gameObject.SetActive(true);
                else 
                {
                    particles[2].Simulate(0f, true, true);
                    particles[2].Play();
                }

                particles[0].Stop();
                break;
            case "PowActive":
                if(!transform.GetChild(1).gameObject.activeInHierarchy) 
                    transform.GetChild(1).gameObject.SetActive(true);
                else
                {
                    particles[1].Simulate(0f, true, true);
                    particles[1].Play();
                }
                break;
            case "PowTaken":
                if(!transform.GetChild(3).gameObject.activeInHierarchy) 
                    transform.GetChild(3).gameObject.SetActive(true);
                else
                {
                    particles[3].Simulate(0f, true, true);
                    particles[3].Play();
                }
                particles[1].Stop();
                break;
        }
    }

    public void SmokeParticles (string state, Vector3 coronaPos)
    {
        switch(state)
        {
            case "CoronaOver":
            break;
            case "PeopleDie":
            break;
        }
    }
}
