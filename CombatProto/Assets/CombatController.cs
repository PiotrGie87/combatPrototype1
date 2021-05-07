using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private PlayerController playerController;
    private EnemyController enemyController;

    private bool isPlayerAction;
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();

        Next();
    }

    public void Next()
    {
        if (isPlayerAction)
        {
            isPlayerAction = false;
            enemyController.StartAttack();
        }
        else
        {
            isPlayerAction = true;
            playerController.StartDefense();
        }
    }
}
