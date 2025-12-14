using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject gameplayPenal;
    [SerializeField] private GameObject winPenel;
    [SerializeField] private GameObject losePenel;

    

    void Awake() => Instance = this;
    void Start() => SwitchPenal(gameplayPenal);
    void SwitchPenal(GameObject _penal)
    {
        CloseAll();
        if(_penal != null)
            _penal.SetActive(true);

    }
    void CloseAll()
    {
        winPenel.SetActive(false);
        losePenel.SetActive(false);
        gameplayPenal.SetActive(false);
    }
    public void OpenWinPenal() => SwitchPenal(winPenel);
    public void OpenLosePenal() => SwitchPenal(losePenel);

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}