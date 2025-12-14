using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckPoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //Check if check point available
        if(currentCheckPoint == null)
        {
            //show game over screen
            uiManager.GameOver();

            return; // don't execute the rest of this function
        }

        transform.position = currentCheckPoint.position; // move player to checkpoint position
        playerHealth.Respawn(); // restore player health and reset animation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckPoint = collision.transform; // store the checkpoint tha we activated as the current one
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
