using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverForm : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleView;

    public static event UnityAction ResetClicked;


    private void Awake()
    {
        _titleView.text = "Best Score: " + PlayerPrefs.GetInt("Best", 0);
    }

    public void OnResetClick()
    {
        ResetClicked?.Invoke();
        Destroy(gameObject);
    }
}
