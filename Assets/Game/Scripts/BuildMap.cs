using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuildMap : MonoBehaviour
{
    [SerializeField] private StartPoint startPoint;
    [SerializeField] private Brick brick;
    [SerializeField] private GameObject path;

    int[][] data;
    private void Start()
    {
        string filePath = @"Assets/Game/File Txt/StackMaker.txt";
        string[] lines = File.ReadAllLines(filePath);

        // Tạo mảng hai chiều với kích thước phù hợp
        data = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            // Phân tách từng dòng thành các phần tử
            string[] values = lines[i].Split(',');

            // Chuyển các phần tử thành số nguyên và lưu vào mảng
            data[i] = Array.ConvertAll(values, int.Parse);
        }
        
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 12; j++)
            {
                if(data[i][j] == 2)
                {
                    Instantiate(startPoint, new Vector3(i, -1.5f, j), Quaternion.Euler(90,0,0));
                    Instantiate(path, new Vector3(i, -2.6f, j), Quaternion.identity);
                }
                if (data[i][j] == 1)
                {
                    Instantiate(brick, new Vector3(i, -1.5f, j), Quaternion.Euler(90, 0, 0));
                    Instantiate(path, new Vector3(i, -2.6f, j), Quaternion.identity);
                }
            }
        }
    }
}

