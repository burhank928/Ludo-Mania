using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    public List<int> numbers;
    public TurnManager turnManager;
    public PlayerManager playerManager;
    public Sprite[] dice_pics;
    public GameObject[] dices;
    public GameObject prefab_Rolled_Dice;
    Vector3 red_Pos;
    Vector3 green_Pos;
    Vector3 yellow_Pos;
    Vector3 blue_Pos;
    public List<GameObject> instants;
    public Canvas canvas;
    public Movement movement;
    bool check = true;

    void Start()
    {
        reset_Pos("red");
        reset_Pos("green");
        reset_Pos("yellow");
        reset_Pos("blue");
    }

    public void roll_Dice()
    {
        //int num = (Random.Range(1, 7));

        int num;

        if (check)
        {
            num = 2;
            check = false;
        }
        else
        {
            num = 4;
            check = true;
        }

        numbers.Add(num);

        if (num != 6)
        {
            if (!movement.possible_To_Move())
            {
                turnManager.Turn_Completed = true;
            }
            else
            {
                turn_Off_Animator_And_Button(turnManager.turn_Of_Player_int());
            }
        }

        GameObject instant = Instantiate(prefab_Rolled_Dice, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        
        instants.Add(instant);
        change_Picture(instant);
        instant.GetComponent<Image>().useSpriteMesh = true;
        instant.GetComponent<Image>().preserveAspect = true;
        set_Position(instant, turnManager.turn_Of());
    }

    void create_Instants()
    {
        while (instants.Count != 0)
        {
            var temp = instants[0];
            instants.RemoveAt(0);
            Destroy(temp);
        }
    }

    public void destroy_Instants()
    {
        while (instants.Count != 0)
        {
            var temp = instants[0];
            instants.RemoveAt(0);
            Destroy(temp);
        }
    }

    public void change_Picture(GameObject obj)
    {
        obj.GetComponent<Image>().sprite = dice_pics[numbers[numbers.Count - 1] - 1];
    }

    public void turn_Off_Animator_And_Button(int index)
    {
        dices[index].GetComponent<Button>().enabled = false;
        dices[index].GetComponent<Animator>().enabled = false;
    }

    public void turn_On_Animator_And_Button(int index)
    {
        dices[index].GetComponent<Button>().enabled = true;
        dices[index].GetComponent<Animator>().enabled = true;
    }

    void set_Position(GameObject obj, string color)
    {
        obj.transform.localPosition = get_Position(color);
    }

    public void remove_Number(int num)
    {
        bool check = true;

        for (int counter = 0; check; counter++)
        {
            if (numbers[counter] == num)
            {
                numbers.RemoveAt(counter);
                var temp = instants[counter];
                instants.RemoveAt(counter);
                Destroy(temp);
                check = false;
            }
        }

        reset_Pos(turnManager.turn_Of());

        for (int counter = 0; counter < instants.Count; counter++)
        {
            set_Position(instants[counter], turnManager.turn_Of());
        }
    }

    Vector3 get_Position(string color)
    {
        Vector3 pos;

        if (color == "red")
        {
            pos = red_Pos;
            red_Pos.x += 50;
            if ((instants.Count) % 5 == 0)
            {
                red_Pos.x = -300;
                red_Pos.y -= 50;
            }
        }
        else if (color == "green")
        {
            pos = green_Pos;
            green_Pos.x += 50;
            if ((instants.Count) % 5 == 0)
            {
                green_Pos.x = -300;
                green_Pos.y += 50;
            }
        }
        else if (color == "yellow")
        {
            pos = yellow_Pos;
            yellow_Pos.x -= 50;
            if ((instants.Count) % 5 == 0)
            {
                yellow_Pos.x = 300;
                yellow_Pos.y += 50;
            }
        }
        else
        {
            pos = blue_Pos;
            blue_Pos.x -= 50;
            if ((instants.Count) % 5 == 0)
            {
                blue_Pos.x = 300;
                blue_Pos.y -= 50;
            }
        }

        return pos;
    }

    public void reset_Pos(string color)
    {
        if (color == "red")
        {
            red_Pos.x = -300;
            red_Pos.y = -430;
            red_Pos.z = 0;
        }
        else if (color == "green")
        {
            green_Pos.x = -300;
            green_Pos.y = 430;
            green_Pos.z = 0;
        }
        else if (color == "yellow")
        {
            yellow_Pos.x = 300;
            yellow_Pos.y = 430;
            yellow_Pos.z = 0;
        }
        else
        {
            blue_Pos.x = 300;
            blue_Pos.y = -430;
            blue_Pos.z = 0;
        }
    }

    public bool is_Six()
    {
        for (int counter = 0; counter < numbers.Count; counter++)
        {
            if (numbers[counter] == 6)
            {
                return true;
            }
        }

        return false;
    }
}