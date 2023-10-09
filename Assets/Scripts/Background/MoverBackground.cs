using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverBackground : MonoBehaviour
{
    public float moveDistanceX = 24f; // ³������ ���� �� X
    public float moveSpeed = 1f; // �������� ����

    private Transform background1;
    private Transform background2;
    [SerializeField] private GameObject Spawner;

    private void Start()
    {
        background1 = transform.Find("Background1");
        background2 = transform.Find("Background2");

        // ����������� ���������� � BackgroundData
        background1.position = new Vector3(BackgroundData.Instance.startX1, background1.position.y, background1.position.z);
        background2.position = new Vector3(BackgroundData.Instance.startX2, background2.position.y, background2.position.z);
    }

    private void FixedUpdate()
    {
        MoveBackground();
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ��������, �� ������ ������� ����� �� ������� 1
        if (activeSceneIndex == 1)
        {
            Spawner.SetActive(false);
        }
        else
            Spawner.SetActive(true);
    }

    private void MoveBackground()
    {
        // ������ ����
        Vector3 targetPosition1 = new Vector3(background1.position.x - moveDistanceX, background1.position.y, background1.position.z);
        Vector3 targetPosition2 = new Vector3(background2.position.x - moveDistanceX, background2.position.y, background2.position.z);
        float step = moveSpeed * Time.fixedDeltaTime;
        background1.position = Vector3.MoveTowards(background1.position, targetPosition1, step);
        background2.position = Vector3.MoveTowards(background2.position, targetPosition2, step);

        // ����������, �� ��������� ������ ������� ������� ����, � ������������� ����
        if (background1.position.x <= BackgroundData.Instance.startX1 - moveDistanceX)
        {
            background1.position = new Vector3(BackgroundData.Instance.startX1, background1.position.y, background1.position.z);
        }

        // ����������, �� ��������� ������ ������� ������� ����, � ������������� ����
        if (background2.position.x <= BackgroundData.Instance.startX2 - moveDistanceX)
        {
            background2.position = new Vector3(BackgroundData.Instance.startX2, background2.position.y, background2.position.z);
        }
    }
}
