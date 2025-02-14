using UnityEngine;
using UnityEngine.UI;

public class CloseCanvas : MonoBehaviour
{
    public Button closeButton; // ссылка на кнопку, которая будет закрывать Canvas
    public Canvas canvas; // ссылка на Canvas, который нужно отключить
    public AudioSource endTurnSound;

    void Start()
    {
        // Добавляем слушатель события нажатия на кнопку
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    void OnCloseButtonClicked()
    {
        // Отключаем компонент Canvas
        canvas.enabled = false;
        endTurnSound.Play();
    }
}
