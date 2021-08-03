using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

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

    public void SmokeParticles (string state, Transform coronaPos, Transform coronaRot)
    {
        switch(state)
        {
            case "CoronaOver":
                if(!transform.GetChild(4).gameObject.activeInHierarchy && coronaPos.gameObject.activeInHierarchy)
                {
                    particles[4].gameObject.transform.position = coronaPos.position;
                    particles[4].gameObject.transform.rotation = coronaPos.rotation;
                    transform.GetChild(4).gameObject.SetActive(true);
                }
                else
                {
                    //Transform to coronaPos and rotate to coronaPos
                    particles[4].gameObject.transform.position = coronaPos.position;
                    particles[4].gameObject.transform.rotation = coronaPos.rotation;
                    particles[4].Simulate(0f, true, true);
                    particles[4].Play();
                }
            break;
            case "PeopleDie":
                if(!transform.GetChild(5).gameObject.activeInHierarchy) 
                {   
                    transform.GetChild(5).gameObject.transform.position = coronaPos.position;
                    transform.GetChild(5).gameObject.transform.rotation = coronaPos.rotation;
                    transform.GetChild(5).gameObject.SetActive(true);
                }
                break;
        }
    }
}
