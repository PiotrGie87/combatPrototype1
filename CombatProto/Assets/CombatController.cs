using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject focus;

    private float focusVal;
    private Slider focusSlider;
    
    private PlayerController playerController;
    private EnemyController enemyController;

    private bool isPlayerAction;

    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
        focusSlider = focus.GetComponent<Slider>();
        
        Next();
    }

    private void Update()
    {
        if (isPlayerAction)
        {
            focusVal = playerController.focus;
        }
        else
        {
            focusVal = enemyController.focus;
        }
        if (focusVal < 0)
        {
            focusVal = 0;
        }
        focusSlider.value = focusVal;
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
        Debug.Log("isPlayerAction");
        Debug.Log(isPlayerAction);
    }
}
