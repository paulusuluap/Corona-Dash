using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static int[] levelNotif = new int[4] {47, 117, 202, 287};
    public static int levelAmount = 4;
    public static int[] newLevelScores = new int[4] {50, 120, 205, 290};
    public static bool[] isLevelPassed;
    FirstPersonController player;

    private void Awake() {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        isLevelPassed = new bool[4] {false, false, false, false};
    }
    public static void SetLevel(int level)
    {
        switch (level)
        {
            case 2:
            FirstPersonController.instance.WalkSpeed = 12.5f;
            FirstPersonController.instance.TurnSpeed = 60f;
            FirstPersonController.instance.FinalTurnSpeed = 120f;
            CoronaManager.current.CoronaSpawnTime = 5f;
            CoronaManager.current.CoronaSpeed = 8f;
            Pooler.current.coronaStage1 = true;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            break;
            case 3:
            FirstPersonController.instance.WalkSpeed = 15f;
            FirstPersonController.instance.TurnSpeed = 75f;
            FirstPersonController.instance.FinalTurnSpeed = 150f;
            CoronaManager.current.CoronaSpawnTime = 4f;
            CoronaManager.current.CoronaSpeed = 8.25f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            break;
            case 4:
            FirstPersonController.instance.WalkSpeed = 17.5f;
            FirstPersonController.instance.TurnSpeed = 85f;
            FirstPersonController.instance.FinalTurnSpeed = 170f;
            CoronaManager.current.CoronaSpeed = 8.5f;
            Pooler.current.coronaStage2 = true;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            break;
            case 5:
            FirstPersonController.instance.WalkSpeed = 20f;
            FirstPersonController.instance.TurnSpeed = 100f;
            FirstPersonController.instance.FinalTurnSpeed = 200f;
            CoronaManager.current.CoronaSpawnTime = 3f;
            CoronaManager.current.CoronaSpeed = 9f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            break;
        }
    }
}
