using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    // activate game over screen

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if pause screen already active => unpause and viceversa
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }


    #region GAME OVER
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit(); // quit the game (only works on build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // exits play mode (will only be executed in the editor) 
        #endif
    }
    #endregion

    #region PAUSE

    public void PauseGame(bool status)
    {
        // if status == true => pause | if status == false => unpause
        pauseScreen.SetActive(status);

        // when pause status is true change timescale to 0 (time stops)
        // when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }

    public void SoundChange()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusiChange()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion


}