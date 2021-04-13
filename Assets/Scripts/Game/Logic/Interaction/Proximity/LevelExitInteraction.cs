using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitInteraction : ProximityInteraction
{
    bool sequence = false;
    Vector2 startPos;
    float startTime;
    Color startColor;
    const float sequenceTime = 1f;
    Player player;
    

    public override void Interact()
    {
        Debug.Log("Level End Sequence! TM");
        player = FindObjectOfType<Player>();
        player.DeactivateControls();
        startTime = Time.unscaledTime;
        startPos = player.transform.position;
        startColor = player.Renderer().color;
        // Fade Mana score as well
        GameManager.Instance.Victory();
        sequence = true;
    }

    private void Update()
    {
        if (sequence)
        {
            player.transform.position = Vector2.Lerp(startPos, transform.position, Time.unscaledTime - startTime / sequenceTime);
            player.Renderer().color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), Time.unscaledTime - startTime / sequenceTime);
        }
    }
}
