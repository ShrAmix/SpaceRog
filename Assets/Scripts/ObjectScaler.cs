using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private float x1 = 11.66f;
    [SerializeField] private float x2 = 6.66f;
    [SerializeField] private float baseScale = 0.02f;
    [SerializeField] private int PlusMinus=1;

    private void Start()
    {
        // ���������� ������ ������ �� ����� ������
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float visibleWidth = Camera.main.orthographicSize * 2.0f * screenWidth / screenHeight;
        visibleWidth /= 2;

        // ���������� ����� ����� ��'���� �� ����� �����
        float newScale = CalculateScale(visibleWidth);

        // ����������� ����� ����� �� ��'����
        //transform.localScale = new Vector3(newScale- 0.00665517f, newScale - 0.00665517f, newScale);
        transform.localScale = new Vector3(newScale*PlusMinus , newScale, newScale);
    }

    private float CalculateScale(float visibleWidth)
    {
        // ���������� ��������� ������ ������ �� ����������� ������
        float widthRatio = (visibleWidth - x2) / (x1 - x2);

        // ���������� ���������� ��������� �� 1 �� 1.333 (1 + 1/3)
        float scaleFactor = 1 + widthRatio / 3;

        // �������� ���������� ��������� �� 1 �� 1.333
        scaleFactor = Mathf.Min(scaleFactor, 1.333f);

        // ���������� ����� ����� ��'����
        float newScale = baseScale * scaleFactor;

        return newScale;
    }
}
