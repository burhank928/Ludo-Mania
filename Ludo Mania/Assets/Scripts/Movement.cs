using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public Dice dice;
    public PlayerManager playerManager;
    public TurnManager turnManager;
    public Transform[] red_path;
    public Transform[] blue_path;
    public Transform[] green_path;
    public Transform[] yellow_path;
    public GameObject[] red_end;
    public GameObject[] blue_end;
    public GameObject[] green_end;
    public GameObject[] yellow_end;
    public Vector3[,] home_Pos;
    int[,] current_pos;
    public GameObject Instant_Obj;
    public List<GameObject> Instants;
    public GameObject selected_Button_GameObject;
    public List<int> players_Not_In_Home_Player;
    public List<int> players_Not_In_Home_Goti;
    
    private void Awake()
    {
        home_Pos = new Vector3[4, 4];
        current_pos = new int[4, 4];
    }

    private void Start()
    {
        home_Pos[0, 0] = turnManager.Red[0].transform.localPosition;
        home_Pos[0, 1] = turnManager.Red[1].transform.localPosition;
        home_Pos[0, 2] = turnManager.Red[2].transform.localPosition;
        home_Pos[0, 3] = turnManager.Red[3].transform.localPosition;

        home_Pos[1, 0] = turnManager.Green[0].transform.localPosition;
        home_Pos[1, 1] = turnManager.Green[1].transform.localPosition;
        home_Pos[1, 2] = turnManager.Green[2].transform.localPosition;
        home_Pos[1, 3] = turnManager.Green[3].transform.localPosition;

        home_Pos[2, 0] = turnManager.Yellow[0].transform.localPosition;
        home_Pos[2, 1] = turnManager.Yellow[1].transform.localPosition;
        home_Pos[2, 2] = turnManager.Yellow[2].transform.localPosition;
        home_Pos[2, 3] = turnManager.Yellow[3].transform.localPosition;

        home_Pos[3, 0] = turnManager.Blue[0].transform.localPosition;
        home_Pos[3, 1] = turnManager.Blue[1].transform.localPosition;
        home_Pos[3, 2] = turnManager.Blue[2].transform.localPosition;
        home_Pos[3, 3] = turnManager.Blue[3].transform.localPosition;

        current_pos[3, 0] = 15;
        int current_Index = current_pos[3, 0];
        current_Index += 0;
        turnManager.Blue[0].gameObject.transform.position = get_Position("blue", current_Index);
        playerManager.goti_Nikli("blue");

        players_Not_In_Home_Player.Add(3);
        players_Not_In_Home_Goti.Add(0);

        current_pos[3, 1] = 15;
        current_Index = current_pos[3, 1];
        current_Index += 0;
        turnManager.Blue[1].gameObject.transform.position = get_Position("blue", current_Index);
        playerManager.goti_Nikli("blue");

        players_Not_In_Home_Player.Add(3);
        players_Not_In_Home_Goti.Add(1);

        current_pos[0, 0] = 2;
        current_Index = current_pos[0, 0];
        current_Index += 0;
        turnManager.Red[0].gameObject.transform.position = get_Position("red", current_Index);
        playerManager.goti_Nikli("red");

        players_Not_In_Home_Player.Add(0);
        players_Not_In_Home_Goti.Add(0);

        current_pos[0, 1] = 0;
        current_Index = current_pos[0, 1];
        current_Index += 0;
        turnManager.Red[1].gameObject.transform.position = get_Position("red", current_Index);
        playerManager.goti_Nikli("red");

        players_Not_In_Home_Player.Add(0);
        players_Not_In_Home_Goti.Add(1);
    }

    public void show_Options(GameObject obj)
    {
        if (obj.transform.localPosition == home_Pos[turnManager.turn_Of_Player_int(), button_No(obj.name)] && dice.is_Six())
        {
            move_Player(obj, 0);
            dice.remove_Number(6);
            playerManager.goti_Nikli(turnManager.turn_Of());
            players_Not_In_Home_Player.Add(turnManager.turn_Of_Player_int());
            players_Not_In_Home_Goti.Add(button_No(obj.name));
        }
        else if (obj.transform.localPosition != home_Pos[turnManager.turn_Of_Player_int(), button_No(obj.name)])
        {
            selected_Button_GameObject = obj;

            int temp = (dice.numbers.Count / 2);
            float x = 0;

            for (int counter = 0; counter < temp; counter++)
            {
                x += 10;
            }

            if (dice.numbers.Count % 2 == 0)
            {
                x -= 5;
            }

            Vector3 pos = obj.transform.localPosition;

            pos.x -= x;
            pos.y += 11;

            for (int counter = 0; counter < dice.numbers.Count; counter++)
            {
                var instant = Instantiate(Instant_Obj, new Vector3(0, 0, 0), Quaternion.identity, obj.transform.parent.transform);
                instant.transform.localPosition = pos;
                instant.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dice.numbers[counter].ToString();
                pos.x += 10;
                Instants.Add(instant);
            }
        }
    }

    public void move_Player(GameObject obj, int num)
    {
        bool check = false;

        set_Position(obj, num, ref check);

        if (!check && (dice.numbers.Count == 0 || !possible_To_Move()))
        {
            turnManager.Turn_Completed = true;
        }
    }

    void set_Position(GameObject obj, int num, ref bool check)
    {
        int current_Index = current_pos[turnManager.turn_Of_Player_int(), button_No(obj.name)];

        if (is_puggai(turnManager.turn_Of_Player_int(), button_No(obj.name), num)) 
        {
            obj.SetActive(false);
            current_pos[turnManager.turn_Of_Player_int(), button_No(obj.name)] = 57;
            Enable_Puggi_Goti(turnManager.turn_Of(), button_No(obj.name));
            playerManager.goti_Puggai(turnManager.turn_Of());

            for (int counter = 0; counter < players_Not_In_Home_Player.Count; counter++)
            {
                if (players_Not_In_Home_Player[counter] == turnManager.turn_Of_Player_int() && players_Not_In_Home_Goti[counter] == button_No(obj.name))
                {
                    players_Not_In_Home_Player.RemoveAt(counter);
                    players_Not_In_Home_Goti.RemoveAt(counter);
                }
            }

            check = true;

            dice.remove_Number(num);
        }
        else if (!check_Any_Jora_In_Way(turnManager.turn_Of_Player_int(), button_No(obj.name), num))
        {
            current_Index += num;
            obj.transform.position = get_Position(turnManager.turn_Of(), current_Index);
            current_pos[turnManager.turn_Of_Player_int(), button_No(obj.name)] += num;

            check_goti_Piti(obj, ref check);

            dice.remove_Number(num);
        }
    }

    Vector3 get_Position(string name, int current_pos_Position_Index)
    {
        if (name == "red")
        {
            return red_path[current_pos_Position_Index].transform.position;
        }
        else if (name == "blue")
        {
            return blue_path[current_pos_Position_Index].transform.position;
        }
        else if (name == "green")
        {
            return green_path[current_pos_Position_Index].transform.position;
        }
        else
        {
            return yellow_path[current_pos_Position_Index].transform.position;
        }
    }

    int button_No(string name)
    {
        if (name == "Button 1")
        {
            return 0;
        }
        else if (name == "Button 2")
        {
            return 1;
        }
        else if (name == "Button 3")
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void destroy_Instants()
    {
        while (Instants.Count != 0)
        {
            var temp = Instants[0];
            Instants.RemoveAt(0);
            Destroy(temp);
        }
    }

    int return_Path_Name(string colour, int player, int goti_Number)
    {
        if (current_pos[player, goti_Number] <= 56)
        {
            if (colour == "red")
            {
                return Convert.ToInt32(red_path[current_pos[player, goti_Number]].name);
            }
            else if (colour == "blue")
            {
                return Convert.ToInt32(blue_path[current_pos[player, goti_Number]].name);
            }
            else if (colour == "green")
            {
                return Convert.ToInt32(green_path[current_pos[player, goti_Number]].name);
            }
            else
            {
                return Convert.ToInt32(yellow_path[current_pos[player, goti_Number]].name);
            }
        }

        return 0;
    }

    bool is_Safe(string colour, int index)
    {
        if (colour == "red" && red_path[index].tag == "safe")
        {
            return true;
        }
        else if (colour == "blue" && blue_path[index].tag == "safe")
        {
            return true;
        }
        else if (colour == "green" && green_path[index].tag == "safe")
        {
            return true;
        }
        else if (colour == "yellow" && yellow_path[index].tag == "safe")
        {
            return true;
        }

        return false;
    }

    public bool possible_To_Move()
    {
        for (int counter = 0; counter < 4; counter++)
        {
            if (!is_Not_Goti_Home(turnManager.turn_Of_Player_int(), counter) && dice.is_Six())
            {
                return true;
            }
            if (is_Not_Goti_Home(turnManager.turn_Of_Player_int(), counter) && check_All_Numbers_For_Goti(turnManager.turn_Of_Player_int(), counter))
            {
                return true;
            }
        }

        return false;
    }

    bool check_All_Numbers_For_Goti(int player, int goti_Number)
    {
        for (int counter = 0; counter < dice.numbers.Count; counter++)
        {
            if (!check_Any_Jora_In_Way(player, goti_Number, dice.numbers[counter]) || is_puggai(player, goti_Number, dice.numbers[counter]))
            {
                return true;
            }
        }

        return false;
    }

    bool check_Any_Jora_In_Way(int player, int goti_Number, int num)
    {
        int source_Path_Name = return_Path_Name(turnManager.color_Of(player), player, goti_Number);

        for (int counter = 1; counter < num; counter++)
        {
            for (int this_player = 0; this_player < 4; this_player++)
            {
                if (player != this_player)
                {
                    int count = get_How_Many_Goti_Are_Here(this_player, source_Path_Name + counter);

                    if (count > 1)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    bool is_Not_Goti_Home(int player, int goti_Number)
    {
        for (int counter = 0; counter < players_Not_In_Home_Player.Count; counter++)
        {
            if (players_Not_In_Home_Player[counter] == player && players_Not_In_Home_Goti[counter] == goti_Number)
            {
                return true;
            }
        }

        return false;
    }

    bool is_puggai(int player, int goti_Number, int num)
    {
        if (current_pos[player, goti_Number] + num == 56)
        {
            return true;
        }

        return false;
    }

    void Enable_Puggi_Goti(string colour, int goti_Number)
    {
        if (colour == "red")
        {
            red_end[goti_Number].SetActive(true);
        }
        else if (colour == "blue")
        {
            blue_end[goti_Number].SetActive(true);
        }
        else if (colour == "green")
        {
            green_end[goti_Number].SetActive(true);
        }
        else if (colour == "yellow")
        {
            yellow_end[goti_Number].SetActive(true);
        }
    }

    // Trying New Way To Check If Token Is Getting Killed.

    void check_goti_Piti(GameObject obj, ref bool check)
    {
        if (!is_Safe(turnManager.turn_Of(), current_pos[turnManager.turn_Of_Player_int(), button_No(obj.name)]))
        {
            int source_Path_Name = return_Path_Name(turnManager.turn_Of(), turnManager.turn_Of_Player_int(), button_No(obj.name));
            int total_Gotis = get_How_Many_Goti_Are_Here(turnManager.turn_Of_Player_int(), source_Path_Name);

            for (int player = 0; player < 4; player++)
            {
                if (turnManager.turn_Of_Player_int() != player)
                {
                    int player_Gotis = get_How_Many_Goti_Are_Here(player, source_Path_Name);
                    if (player_Gotis > 0 && total_Gotis >= player_Gotis)
                    {
                        goti_Piti(player, source_Path_Name);
                        check = true;
                    }
                }
            }
        }
    }

    void goti_Piti(int player, int source_Path_Name)
    {
        for (int goti_Number = 0; goti_Number < 4; goti_Number++)
        {
            if (return_Path_Name(turnManager.color_Of(player), player, goti_Number) == source_Path_Name)
            {
                var compenent = turnManager.return_Button(turnManager.color_Of(player), goti_Number);

                compenent.gameObject.transform.localPosition = home_Pos[player, goti_Number];
                playerManager.goti_Pitgai(turnManager.color_Of(player));
                current_pos[player, goti_Number] = 0;

                for (int counter = 0; counter < players_Not_In_Home_Player.Count; counter++)
                {
                    if (players_Not_In_Home_Player[counter] == player && players_Not_In_Home_Goti[counter] == goti_Number)
                    {
                        players_Not_In_Home_Player.RemoveAt(counter);
                        players_Not_In_Home_Goti.RemoveAt(counter);
                    }
                }
            }
        }
    }

    int get_How_Many_Goti_Are_Here(int player, int source_Path_Name)
    {
        int count = 0;
        int current_Player_Path;

        for (int goti_Number = 0; goti_Number < 4; goti_Number++)
        {
            current_Player_Path = return_Path_Name(turnManager.color_Of(player), player, goti_Number);
            if (current_Player_Path == source_Path_Name)
            {
                count++;
            }
        }

        return count;
    }
}