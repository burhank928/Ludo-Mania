using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    int player;
    int number_Of_Players;
    bool turn_Completed;
    public Dice dice;
    public Button[] Red;
    public Button[] Green;
    public Button[] Yellow;
    public Button[] Blue;
    public PlayerManager playerManager;

    public int Number_Of_Players
    {
        get { return number_Of_Players; }
    }

    public bool Turn_Completed
    {
        set { turn_Completed = value; }
        get { return turn_Completed; }
    }

    private void Awake()
    {
        turn_Completed = false;
        number_Of_Players = 4;
        //player = Random.Range(0, number_Of_Players);

        player = 0;

        turn_Other_Players_Buttons_off();
        dice.turn_On_Animator_And_Button(player);
    }

    private void Update()
    {
        if (turn_Completed || playerManager.all_puggai(turn_Of()))
        {
            dice.numbers.Clear();
            dice.turn_Off_Animator_And_Button(player);
            player++;
            player %= number_Of_Players;
            dice.turn_On_Animator_And_Button(player);
            turn_Other_Players_Buttons_off();
            dice.destroy_Instants();
            dice.reset_Pos(turn_Of());
            turn_Completed = false;
        }
    }

    public string turn_Of()
    {
        if (player == 0)
        {
            return "red";
        }
        else if(player == 1)
        {
            return "green";
        }
        else if (player == 2)
        {
            return "yellow";
        }
        else
        {
            return "blue";
        }
    }

    public string color_Of(int num)
    {
        if (num == 0)
        {
            return "red";
        }
        else if (num == 1)
        {
            return "green";
        }
        else if (num == 2)
        {
            return "yellow";
        }
        else
        {
            return "blue";
        }
    }

    public int turn_Of_Player_int()
    {
        return player;
    }

    void turn_Other_Players_Buttons_off()
    {
        if (player == 0)
        {
            for (int counter = 0; counter < 4; counter++)
            {
                Red[counter].gameObject.GetComponent<Image>().raycastTarget = true;
                Green[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Yellow[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Blue[counter].gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }
        else if (player == 1)
        {
            for (int counter = 0; counter < 4; counter++)
            {
                Red[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Green[counter].gameObject.GetComponent<Image>().raycastTarget = true;
                Yellow[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Blue[counter].gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }
        else if (player == 2)
        {
            for (int counter = 0; counter < 4; counter++)
            {
                Red[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Green[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Yellow[counter].gameObject.GetComponent<Image>().raycastTarget = true;
                Blue[counter].gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }
        else
        {
            for (int counter = 0; counter < 4; counter++)
            {
                Red[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Green[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Yellow[counter].gameObject.GetComponent<Image>().raycastTarget = false;
                Blue[counter].gameObject.GetComponent<Image>().raycastTarget = true;
            }
        }
    }

    public Button return_Button(string color, int index)
    {
        if (color == "red")
        {
            return Red[index];
        }
        else if (color == "green")
        {
            return Green[index];
        }
        else if (color == "yellow")
        {
            return Yellow[index];
        }
        else
        {
            return Blue[index];
        }
    }
}