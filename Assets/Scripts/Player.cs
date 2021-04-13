using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Controller;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerCharacterController controller;
    [SerializeField] PlayerTargetingSystem targetingSystem;
    [SerializeField] InteractSkill interactSkill;
    [SerializeField] MagicSkill magicSkill;
    [SerializeField] DamageController damageController;

    [SerializeField] SpriteRenderer renderer;

    public void DeactivateControls()
    {
        controller.enabled = false;
        interactSkill.enabled = false;
        targetingSystem.enabled = false;
        magicSkill.enabled = false;
        damageController.enabled = false;
    }
    public void ActivateControls()
    {
        controller.enabled = true;
        interactSkill.enabled = true;
        targetingSystem.enabled = true;
        magicSkill.enabled = true;
        damageController.enabled = true;
    }

    public SpriteRenderer Renderer() {
        return renderer;
    }

    public DamageController GetDamageController()
    {
        return damageController;
    }

    public MagicSkill GetTelekinesis()
    {
        return magicSkill;
    }
}
