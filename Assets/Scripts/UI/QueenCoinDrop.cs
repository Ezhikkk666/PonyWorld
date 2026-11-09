
using UnityEngine;
using System.Collections;

public class QueenCoinDrop : MonoBehaviour
{
    [Header("Настройки")]
    public RectTransform coinPrefab;
    public RectTransform canvas;

    [Header("Количество монет")]
    public int minCoins = 8;
    public int maxCoins = 14;

    [Header("Разлёт")]
    public float minSpeed = 250f;
    public float maxSpeed = 500f;
    public float gravity = 800f;

    [Header("Размер")]
    public float coinSize = 45f;

    public void DropCoins()
    {
        int count = Random.Range(minCoins, maxCoins + 1);

        for (int i = 0; i < count; i++)
        {
            RectTransform coin = Instantiate(
                coinPrefab,
                canvas,
                false
            );

            Vector2 screenPosition =
                RectTransformUtility.WorldToScreenPoint(
                    canvas.GetComponentInParent<Canvas>().worldCamera,
                    transform.position
                );

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas,
                screenPosition,
                canvas.GetComponentInParent<Canvas>().renderMode
                    == RenderMode.ScreenSpaceOverlay
                    ? null
                    : canvas.GetComponentInParent<Canvas>().worldCamera,
                out Vector2 localPosition
            );

            coin.anchoredPosition = localPosition;
            coin.sizeDelta = new Vector2(coinSize, coinSize);

            StartCoroutine(AnimateCoin(coin));
        }
    }

    IEnumerator AnimateCoin(RectTransform coin)
    {
        float angle = Random.Range(0f, 360f);
        float speed = Random.Range(minSpeed, maxSpeed);

        Vector2 direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        );

        Vector2 velocity = direction * speed;

        float rotationSpeed = Random.Range(-600f, 600f);
        float lifetime = 1.3f;
        float elapsed = 0f;

        while (elapsed < lifetime)
        {
            float dt = Time.deltaTime;
            elapsed += dt;

            velocity.y -= gravity * dt;
            coin.anchoredPosition += velocity * dt;

            coin.Rotate(0f, 0f, rotationSpeed * dt);

            float scale = Mathf.Sin(
                Mathf.Clamp01(elapsed / lifetime) * Mathf.PI * 0.5f
            );

            coin.localScale = Vector3.one * scale;

            yield return null;
        }

        Destroy(coin.gameObject);
    }
}
