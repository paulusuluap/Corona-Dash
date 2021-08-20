using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager current;
    private static Animator playerAnim;
    private static Animator[] menuAnims;
    private int[] idleCollections = new int[4] {1, 2, 3, 7};

    private void Awake() {
        current = this;

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            menuAnims = GameObject.FindObjectsOfType<Animator>();
            foreach(var a in menuAnims) a.SetFloat("Speed_f", 0.2f);
            StartCoroutine(RandomMenuAnims());
        }
        else 
        {
            playerAnim = GameObject.FindWithTag("Characters").GetComponent<Animator>();
            SetAnim("Running");
        }
    }

    public static void SetAnim(string whatIsCondition)
    {
        switch(whatIsCondition)
        {
            case "Idle":
                playerAnim.SetFloat("Speed_f", 0.2f);
                break;
            case "Running":
                playerAnim.SetFloat("Speed_f", 0.6f);
                break;
            case "Die":
                playerAnim.SetBool("Death_b" ,true);
                break;
            case "DeathType1":
                playerAnim.SetInteger("DeathType_int" , 1);
                break;
            case "DeathType2":
                playerAnim.SetInteger("DeathType_int" , 2);
                break;
            case "LookStraight":
                playerAnim.SetFloat("Head_Horizontal_f", 0f);
                break;
            case "LookLeft":
                float lookLeft = Mathf.Lerp(0f, -0.3f, 30f * Time.deltaTime);
                playerAnim.SetFloat("Head_Horizontal_f", lookLeft);
                break;
            case "LookRight":
                float LookRight = Mathf.Lerp(0f, 0.3f, 30f * Time.deltaTime);
                playerAnim.SetFloat("Head_Horizontal_f", LookRight);
                break;
        }
    }

    private static void SetIdleRandom(int random)
    {
        foreach(Animator a in menuAnims)
        a.SetInteger("Animation_int", random);
    }

    private IEnumerator RandomMenuAnims()
    {
        int random = Random.Range(0, 4);
        SetIdleRandom(idleCollections[random]);

        yield return new WaitForSeconds(2f);

        SetIdleRandom(0);

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(RandomMenuAnims());
    }
}
