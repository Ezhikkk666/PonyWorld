using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RandomHeartSprites : MonoBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        var tsa = ps.textureSheetAnimation;

        tsa.enabled = true;
        tsa.mode = ParticleSystemAnimationMode.Sprites;

        // Случайное начальное сердечко
        tsa.startFrame = new ParticleSystem.MinMaxCurve(0f, 1f);

        // Не менять сердечко во время падения
        tsa.frameOverTime = new ParticleSystem.MinMaxCurve(0f);
    }
}