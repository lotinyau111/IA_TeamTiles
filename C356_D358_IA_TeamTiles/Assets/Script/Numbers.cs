using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{

    public GameObject gameManager;
    //public GameObject _00_num0, _00_num1, _00_num2, _00_num3, _00_num4, _00_num5, _00_num6, _00_num7, _00_num8, _00_num9, _00_numA, _00_numB;
    public GameObject[] _00_num, _01_num, _02_num, _03_num, _04_num, _05_num,
                        _10_num, _11_num, _12_num, _13_num, _14_num, _15_num,
                        _20_num, _21_num, _22_num, _23_num, _24_num, _25_num,
                        _30_num, _31_num, _32_num, _33_num, _34_num, _35_num,
                        _40_num, _41_num, _42_num, _43_num, _44_num, _45_num,
                        _50_num, _51_num, _52_num, _53_num, _54_num, _55_num;

    public GameObject[][] _all_num;
    private string[] gameTableContents = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B" };
    // Start is called before the first frame update
    void Start()
    {
        //  _00_num = new GameObject[] { _00_num0, _00_num1, _00_num2, _00_num3, _00_num4, _00_num5, _00_num6, _00_num7, _00_num8, _00_num9, _00_numA, _00_numB };
        _all_num = new GameObject[][] {_00_num, _01_num, _02_num, _03_num, _04_num, _05_num,
                        _10_num, _11_num, _12_num, _13_num, _14_num, _15_num,
                        _20_num, _21_num, _22_num, _23_num, _24_num, _25_num,
                        _30_num, _31_num, _32_num, _33_num, _34_num, _35_num,
                        _40_num, _41_num, _42_num, _43_num, _44_num, _45_num,
                        _50_num, _51_num, _52_num, _53_num, _54_num, _55_num    };
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0, y = 0;
        for (int j = 0; j < _all_num.Length; j++)
        {
            for (int i = 0; i < _all_num[j].Length; i++)
            {
                if (gameManager.GetComponent<SetGameBoard>().gameTable[x][y].GetComponent<Text>().text == gameTableContents[i])
                {
                    _all_num[j][i].SetActive(true);
                    _all_num[j][i].transform.Rotate(new Vector3(0, 0.75f, 0));
                    Debug.Log("X" + x + "y" + y + "j" + j + "i" + i);
                }
                else _all_num[j][i].SetActive(false);
            }
            if (y < 5) y++;
            else { y = 0; x++; }

        }
    }
}
