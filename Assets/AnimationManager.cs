using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private static Animator playerAnim;
    public GameObject m_PlayerCharacter;

    private void Start() {
        playerAnim = m_PlayerCharacter.GetComponent<Animator>();
    }

    public static void SetAnim(string whatIsCondition)
    {
        switch(whatIsCondition)
        {
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
            case "LookLeft":
                float lookLeft = Mathf.Lerp(0f, -0.3f, 1f * Time.deltaTime);
                playerAnim.SetFloat("Head_Horizontal_f", lookLeft);
                break;
            case "LookRight":
                float LookRight = Mathf.Lerp(0f, 0.3f, 1f * Time.deltaTime);
                playerAnim.SetFloat("Head_Horizontal_f", LookRight);
                break;
            
        }
    }
}
