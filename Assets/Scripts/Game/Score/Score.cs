using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class Score
    {
        static int mana;
        static int maxMana;
        static float time;
        static int pickups;
        static int enemies;

        static int maxTimeScore = 100;

        public static void SetTimeScorePool(int maxTimeScore)
        {
            Score.maxTimeScore = maxTimeScore;
        }

        public static int MaxMana { get => maxMana; set => maxMana = value; }

        public static void SetMana(int x)
        {
            mana = x;
        }

        public static int GetMana()
        {
            return mana;
        }

        public static void AddMana(int x)
        {
            if (x < 0)
                GameManager.Instance.ManaRegenCooldown();

            mana += x;
            if (mana < 0)
                mana = 0;
            if (mana > maxMana * 2)
                mana = maxMana * 2;
        }

        public static void ResetTime()
        {
            time = 0f;
        }

        public static void AddTime(float deltaTime)
        {
            time += deltaTime;
        }

        public static int GetTimeScore()
        {
            return Mathf.Clamp(maxTimeScore - Mathf.FloorToInt(time), 0 , maxTimeScore);
        }

        public static float GetTimeValue()
        {
            return time;
        }

        public static void SetPickup(int x)
        {
            pickups = x;
        }

        public static int GetPickup()
        {
            return pickups;
        }

        public static void AddPickup(int score)
        {
            pickups += score;
        }

        public static void SetEnemies(int x)
        {
            enemies = x;
        }

        public static int GetEnemies()
        {
            return enemies;
        }

        public static void AddEnemies(int score)
        {
            enemies += score;
        }

        public static int GetScore()
        {
            return GetPickup() + GetEnemies() + GetTimeScore();
        }
    }
}