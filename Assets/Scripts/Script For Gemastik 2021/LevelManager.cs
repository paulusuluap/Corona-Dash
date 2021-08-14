using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    // public static int[] levelNotif = new int[4] {47, 117, 202, 287};
    public static int[] levelNotif = new int[4] {7, 17, 27, 37};
    public static int levelAmount = 4;
    public static int[] newLevelScores = new int[4] {10, 20, 30, 40};
    // public static int[] newLevelScores = new int[4] {50, 120, 205, 290};
    public static bool[] isLevelPassed;
    public static int nextLevelCounter = 3;
    FirstPersonController player;

    private void Awake() {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        isLevelPassed = new bool[4] {false, false, false, false};
    }
    public void SetLevel(int level)
    {
        switch (level)
        {
            case 2:
            player.WalkSpeed = 12.5f;
            player.TurnSpeed = 225f;
            CoronaManager.current.CoronaSpawnTime = 5f;
            CoronaManager.current.CoronaSpeed = 8f;
            Pooler.current.coronaStage1 = true;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            break;
            case 3:
            player.WalkSpeed = 15f;
            player.TurnSpeed = 270f;
            CoronaManager.current.CoronaSpawnTime = 4f;
            CoronaManager.current.CoronaSpeed = 8.25f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            break;
            case 4:
            player.WalkSpeed = 17.5f;
            player.TurnSpeed = 315f;
            CoronaManager.current.CoronaSpeed = 8.5f;
            Pooler.current.coronaStage2 = true;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            break;
            case 5:
            player.WalkSpeed = 20f;
            player.TurnSpeed = 360f;
            CoronaManager.current.CoronaSpawnTime = 3f;
            CoronaManager.current.CoronaSpeed = 9f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            break;
        }
    }
}
