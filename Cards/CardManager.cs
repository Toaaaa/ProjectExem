using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour //���� ����, �Ʒü� ui���� ����� ī�� �����͸� �����ϴ� �Ŵ���.
{
    //��ų ī�� + ���� ���ο��� ȹ���ϴ� ��� ī��(���� ���) + ���濡 ������ ������ ��� ī��
    public List<CardData> cardDataList;//��ų ī�� ������
    public List<CardData> dungeonCardList;//�������� ȹ���ϴ� ī�� ������(cardData�� utility�� �ش���)
    public List<CardData> itemCardList;//���濡 ������ ������ ī�� ������

    public List<CardData> availableCardList;// ��� ������ ī�� ����Ʈ(���õ� �������� ���� ���� ī��� ����)

    public bool isCardUpdate;//ó���� ī�尡 ������Ʈ�� �� ���� ������ �۾��� �����ϵ��� �ϴ� ����.

    private void Start()
    {
        //���濡 ������ �������� ī��� ��ȯ�� itemCardList�� �߰��ϴ� ��ũ��Ʈ ���� �߰�.
        UpdateAvailableCards();
    }

    // ��� ������ ī�� ������Ʈ
    public void UpdateAvailableCards()
    {
        availableCardList = cardDataList.FindAll(card => !card.islocked);
        availableCardList.AddRange(dungeonCardList.FindAll(card => !card.islocked));
        //������ �������� ī��� ��ȯ�ϴ� ��ũ��Ʈ ���� �߰�.

        isCardUpdate = true;
    }

    // Ư�� ī�� ��� ����
    public void UnlockCard(CardData cardData)
    {
        if (cardData.islocked)
        {
            cardData.islocked = false;
            availableCardList.Add(cardData);
        }
    }

    // Ư�� ī�� ���
    public void LockCard(CardData cardData)
    {
        if (!cardData.islocked)
        {
            cardData.islocked = true;
            availableCardList.Remove(cardData);
        }
    }
}
