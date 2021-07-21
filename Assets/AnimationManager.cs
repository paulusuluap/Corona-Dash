using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager current;
    private Animator playerAnim;
    public GameObject m_PlayerCharacter;

    private void Start() {
        current = this;
        playerAnim = m_PlayerCharacter.GetComponent<Animator>();
    }

    public void SetAnim(string whatIsCondition)
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
            case "Happy":
                //PlaySound Ayayay or Yohoo (Male/Female)
                playerAnim.SetTrigger("Jump_trig");
                break;
            
        }
    }
}
