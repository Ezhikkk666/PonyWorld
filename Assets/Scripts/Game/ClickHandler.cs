using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public void OnClick()
    {
        GameData.Instance.AddResource1(GameData.Instance.ClickPower);

       // Если понадобится проверка:

       // Debug.Log("ClickHandler.OnClick вызван");
       // if (GameData.Instance == null)
       // {
       //     Debug.LogError("GameData.Instance == null");
       //     return;
       // }
       // Debug.Log($"Было: {GameData.Instance.Resource1Amount}, добавляю: {GameData.Instance.ClickPower}");
       // GameData.Instance.AddResource1(GameData.Instance.ClickPower);
       // Debug.Log($"Стало: {GameData.Instance.Resource1Amount}");
    }
  
}
