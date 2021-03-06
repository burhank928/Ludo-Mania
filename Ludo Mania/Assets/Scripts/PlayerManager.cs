﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int total_Red_Goti;
    int total_Blue_Goti;
    int total_Green_Goti;
    int total_Yellow_Goti;

    int red_Goti_In_Home;
    int blue_Goti_In_Home;
    int green_Goti_In_Home;
    int yellow_Goti_In_Home;

    public Dice dice;
    public TurnManager turnManager;

    public List<string> winners;

    private void Awake()
    {
        total_Blue_Goti = 4;
        total_Green_Goti = 4;
        total_Red_Goti = 4;
        total_Yellow_Goti = 4;

        red_Goti_In_Home = 4;
        blue_Goti_In_Home = 4;
        green_Goti_In_Home = 4;
        yellow_Goti_In_Home = 4;
    }

    private void Update()
    {
        if (total_Red_Goti == 0 && total_Green_Goti == 0 && total_Yellow_Goti == 0 && total_Blue_Goti == 0)
        {
            for (int counter = 0; counter < winners.Count; counter++)
            {
                Debug.Log(winners[counter]);
            }
            Debug.Log("GameOver");
        }
    }

    public bool all_In_Home(string colour)
    {
        if (colour == "red" && red_Goti_In_Home == total_Red_Goti)
        {
            return true;
        }
        else if (colour == "blue" && blue_Goti_In_Home == total_Blue_Goti)
        {
            return true;
        }
        else if (colour == "green" && green_Goti_In_Home == total_Green_Goti)
        {
            return true;
        }
        else if (colour == "yellow" && yellow_Goti_In_Home == total_Yellow_Goti)
        {
            return true;
        }

        return false;
    }

    public bool all_puggai(string colour)
    {
        if (colour == "red" && total_Red_Goti == 0)
        {
            return true;
        }
        else if (colour == "blue" && total_Blue_Goti == 0)
        {
            return true;
        }
        else if (colour == "green" && total_Green_Goti == 0)
        {
            return true;
        }
        else if (colour == "yellow" && total_Yellow_Goti == 0)
        {
            return true;
        }

        return false;
    }

    public void goti_Puggai(string colour)
    {
        if (colour == "red" && total_Red_Goti > 0)
        {
            total_Red_Goti--;
        }
        else if (colour == "blue" && total_Blue_Goti > 0)
        {
            total_Blue_Goti--;
        }
        else if (colour == "green" && total_Green_Goti > 0)
        {
            total_Green_Goti--;
        }
        else if (colour == "yellow" && total_Yellow_Goti > 0)
        {
            total_Yellow_Goti--;
        }

        if (all_puggai(colour))
        {
            winners.Add(colour);
        }

        dice.turn_On_Animator_And_Button(turnManager.turn_Of_Player_int());
    }

    public void goti_Pitgai(string colour)
    {
        if (colour == "red" && red_Goti_In_Home < 4)
        {
            red_Goti_In_Home++;
        }
        else if (colour == "blue" && blue_Goti_In_Home < 4)
        {
            blue_Goti_In_Home++;
        }
        else if (colour == "green" && green_Goti_In_Home < 4)
        {
            green_Goti_In_Home++;
        }
        else if (colour == "yellow" && yellow_Goti_In_Home < 4)
        {
            yellow_Goti_In_Home++;
        }

        dice.turn_On_Animator_And_Button(turnManager.turn_Of_Player_int());
    }

    public void goti_Nikli(string colour)
    {
        if (colour == "red" && red_Goti_In_Home > 0)
        {
            red_Goti_In_Home--;
        }
        else if (colour == "blue" && blue_Goti_In_Home > 0)
        {
            blue_Goti_In_Home--;
        }
        else if (colour == "green" && green_Goti_In_Home > 0)
        {
            green_Goti_In_Home--;
        }
        else if (colour == "yellow" && yellow_Goti_In_Home > 0)
        {
            yellow_Goti_In_Home--;
        }
    }
}
