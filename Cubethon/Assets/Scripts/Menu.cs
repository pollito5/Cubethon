using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartCubethonButton_Handler()
    {
        SceneManager.LoadScene("Level01");
    }

    public void StartSeekFleeButton_Handler()
    {
        SceneManager.LoadScene("Seek_Flee");
    }
}
