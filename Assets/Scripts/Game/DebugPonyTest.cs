using UnityEngine;

public class DebugPonyTest : MonoBehaviour
{
    // проверка
    private float _logTimer;
  
    private void Update()
    {
        _logTimer += Time.deltaTime;
        if (_logTimer < 2f)
            return;
        _logTimer = 0f;

        var data = GameData.Instance;
        Debug.Log($"Coins: {data.Resource1Amount}, Click Power: {data.ClickPower}");

        if (Random.value < 0.3f && PonyManager.Instance.AllPonies.Count > 0 )
        {
            var pony = PonyManager.Instance.AllPonies[Random.Range(0, PonyManager.Instance.AllPonies.Count)];
            Debug.Log($"Buy/Upgrade {pony.DisplayName} (price {PonyManager.Instance.GetCurrentPrice(pony)}) -> {PonyManager.Instance.TryBuyOrUpgrade(pony.Id)}");
        }
    }
}
