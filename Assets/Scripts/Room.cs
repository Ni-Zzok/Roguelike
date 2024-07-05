using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject RoomPrefab; // ������ ������ �����
    public int width = 22; // ������ ������� � ������
    public int height = 14; // ������ ������� � ������

    //public GameObject LeftDoor; // ����� ������
    //public GameObject RightDoor; // ����� �������
    //public GameObject UpDoor; // ����� �����
    //public GameObject DownDoor; // ����� ����

    private void Start()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        // ���������, ��� ������ ����� ��� ��������
        if (RoomPrefab == null)
        {
            Debug.LogError("Tile Prefab is not assigned in the Room script.");
            return;
        }

        // ��������� ������ �������
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                Instantiate(RoomPrefab, position, Quaternion.identity, transform);
            }
        }

        //// ���������������� ������
        //if (LeftDoor != null)
        //    PositionDoor(LeftDoor, new Vector3(7.2738f, -0.01865f, 0.0000323824f)); // ��������� ������� ��� LeftDoor
        //if (RightDoor != null)
        //    PositionDoor(RightDoor, new Vector3(width - 0.5f, 0, height / 2));
        //if (UpDoor != null)
        //    PositionDoor(UpDoor, new Vector3(width / 2, 0, height - 0.5f));
        //if (DownDoor != null)
        //    PositionDoor(DownDoor, new Vector3(width / 2, 0, -0.5f));
    }

    //private void PositionDoor(GameObject door, Vector3 position)
    //{
    //    Instantiate(door, position, Quaternion.identity, transform);
    //}
}
