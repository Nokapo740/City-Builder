using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private City city;

    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text cityText;

	// Use this for initialization
	void Start () {
        city = GetComponent<City>();

        UpdateCityData();
    }

    public void UpdateDayCount() {
        dayText.text = "День " + city.Day;
    }

    public void UpdateCityData() {
        cityText.text = string.Format(
            "Капитал: ${0} (+${1})\nПопуляция: {2}/{3}\nЗапасы еды: {4}\nРабочие места: {5}/{6}\nСчастье жителей: {7}",
            city.Cash, city.JobsCurrent * 2,
            (int)city.PopulationCurrent, (int)city.PopulationCeiling, 
            (int)city.Food,
            city.JobsCurrent, city.JobsCeiling, city.Happiness
        );
    }
}
