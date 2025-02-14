using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas;

    private City city;

    void Start()
    {
        // Получаем ссылку на компонент City в сцене
        city = FindObjectOfType<City>();

        // Проверяем исходное значение переменной Over и соответственно включаем или выключаем Canvas
        if (city.Over)
        {
            EnableCanvas();
        }
        else
        {
            DisableCanvas();
        }
    }

    void Update()
    {
        // Проверяем изменение переменной Over и включаем или выключаем Canvas в зависимости от этого
        if (city.Over)
        {
            EnableCanvas();
        }
        else
        {
            DisableCanvas();
        }
    }

    // Метод для включения Canvas
    void EnableCanvas()
    {
        canvas.enabled = true;
    }

    // Метод для выключения Canvas
    void DisableCanvas()
    {
        canvas.enabled = false;
    }
}
