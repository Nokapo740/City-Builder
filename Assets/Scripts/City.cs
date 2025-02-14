using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {

    public int Cash { get; set; }
    public int Day { get; set; }
    public int Happiness { get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[4];

    public AudioSource endTurnSound;

    private UIController uiController;

    public bool Over { get; private set; }
    public bool Win { get; private set; }

	// Use this for initialization
	void Start () {
        uiController = GetComponent<UIController>();

        Cash = 600;
        Food = 50;
        Happiness = 50;
        Over = false; 
        Win = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void EndTurn() {
        Day++;

        CalculateCash();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculateHappiness();

        Debug.Log("Day ended.");

        uiController.UpdateCityData();
        uiController.UpdateDayCount();

        Debug.LogFormat(
            "Jobs: {0}/{1}, Cash: {2}, Population: {3}/{4}, Food: {5}, Happiness: {6}",
            JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food, Happiness
        );

        if (PopulationCurrent == 0 && Day > 1 || Cash == 0 && JobsCurrent == 0)
        {
            Debug.Log("Game Over");
            Over = true;
        }
        if (Day == 150)
        {
            Debug.Log("You Win");
            Win = true;
            Over = false;
        }

        endTurnSound.Play();
    }

    void CalculateJobs() {
        JobsCeiling = buildingCounts[3] * 10;
        JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
    }

    void CalculateCash() {
        Cash += JobsCurrent * 2;
    }

    public void DepositCash(int cash) {
        Cash += cash;
    }

    void CalculateFood() {
        Food += buildingCounts[2] * 4f - PopulationCurrent;
        if (Food < 0)
            Food = 0;
    }

    void CalculatePopulation() {
        PopulationCeiling = buildingCounts[1] * 5;
        
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling) {
            Food -= PopulationCurrent * 0.25f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * 0.25f, PopulationCeiling);
        } else if (Food < PopulationCurrent) {
            PopulationCurrent -= PopulationCurrent - Food;
            if (PopulationCurrent < 0)
                PopulationCurrent = 0;
        }
        if (Happiness < 25) PopulationCurrent -= (int)(PopulationCurrent * 0.25); // Уходит 25% населения при уровне счастья ниже 25
        if (Happiness < 50) PopulationCurrent -= (int)(PopulationCurrent * 0.08); // Уходит 8% населения при уровне счастья ниже 50
        if (Happiness < 20) PopulationCurrent -= (int)(PopulationCurrent * 0.9);
        if (Happiness > 90) PopulationCurrent += (int)(PopulationCurrent * 0.05); // Прибавляется 2% населения при уровне счастья выше 90

        if (PopulationCurrent > PopulationCeiling) 
            PopulationCurrent = PopulationCeiling;

        if (Over)
        {
            PopulationCurrent = 0;
        }
    }

    void CalculateHappiness()
    {
        if (Food < PopulationCurrent)
        {
            Happiness -= 10;
        } else if (Food == 0) {
            Happiness -= 15;
        } else { Happiness += 3;}

        if (JobsCeiling < PopulationCurrent)
        {
            Happiness -= 2;
        } else
        {
            Happiness += 1;
        }

        if (Happiness < 0) 
            Happiness = 0;
        if (Happiness > 100)
            Happiness = 100;
    }
}
