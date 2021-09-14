using UnityEngine;
using System;

public class Corona : MonoBehaviour
{
    public static event Action CoinPickedUp = delegate{};
    private GravityAttractor planet;
    private Rigidbody rb;
    
    private void OnEnable() {   
        rb = this.gameObject.GetComponent<Rigidbody>();
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
    }

    private void OnDisable() {
        ParticleManager.instance.SmokeParticles("CoronaOver", this.transform, this.transform);
    }

    private void OnCollisionEnter(Collision col) {   
        if(col.gameObject.CompareTag("Player") && !PowerUpsController.Instance.IsMaskActive)
        {
            col.gameObject.GetComponent<FirstPersonController>().enabled = false;

            if(UIManager.current.SceneName == "World_3")
            AudioManager.PlaySound("FemaleCorona");
            else
            AudioManager.PlaySound("MaleCorona");
            
            AnimationManager.SetAnim("DeathType2");
            AnimationManager.SetAnim("Die");
            UIManager.current.EndUI();

            AudioManager.Vibrate();
            Destroy();
        }
        //Immune to CoronaVirus
        else if(col.gameObject.CompareTag("Player") && PowerUpsController.Instance.IsMaskActive)
        {
            //Play satisfying corona sound hitting player
            //Add extra coin to player everytime corona hit

            CoinPickedUp();
            CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
            AudioManager.PlaySound("TakeCoin");
            AudioManager.PlaySound("CoronaHit");
            
            ParticleManager.instance.SmokeParticles("CoronaOver", this.transform, this.transform);
            
            this.gameObject.SetActive(false);    
        }
    }

    private void Destroy() {
        ParticleManager.instance.SmokeParticles("PeopleDie", this.transform, this.transform);
        this.gameObject.SetActive(false);
    }

    public void CoronaMoveToPlayer(FirstPersonController player, float coronaSpeed)
    {
        if(!this.gameObject.activeInHierarchy || !player.enabled) return;

        Vector3 chasePlayer = Vector3.MoveTowards(this.transform.position, player.transform.position, coronaSpeed * Time.deltaTime);
        rb.MovePosition(chasePlayer);
    }

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(this.transform.position ,new Vector3 (15.0f, 15.0f, 15.0f));
    // }
}
