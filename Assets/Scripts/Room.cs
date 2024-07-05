using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject tilePrefab; // ������ ������ �����
    public int width = 22; // ������ ������� � ������
    public int height = 14; // ������ ������� � ������

    public GameObject LeftDoor; // ����� ������
    public GameObject RightDoor; // ����� �������
    public GameObject UpDoor; // ����� �����
    public GameObject DownDoor; // ����� ����

    private void Start()
    {
        GenerateRoom();
    }

    private void GenerateRoom()
    {
        // ��������� ������ �������
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }

        // ���������������� ������
        if (LeftDoor != null)
            PositionDoor(LeftDoor, new Vector3(7.273801f, -0.01865143f, 0.0003238246f)); // ��������� ������� ��� LeftDoor
        if (RightDoor != null)
            PositionDoor(RightDoor, new Vector3(-1.343711f, -0.2632381f, 0.02746211f));
        if (UpDoor != null)
            PositionDoor(UpDoor, new Vector3(5.207458f, 0.3891273f, -0.01900205f));
        if (DownDoor != null)
            PositionDoor(DownDoor, new Vector3(2.942336f, -3.212335f, 0.02313569f));
    }

    private void PositionDoor(GameObject door, Vector3 position)
    {
        Instantiate(door, position, Quaternion.identity, transform);
    }
}
