using System.Collections.Generic;
using Client.DevTools.MyTools;
using UnityEngine;

namespace Client.Data
{
    public static class Sounds
    {
        public static readonly string MusicGameplaySound = "ost";
        public static readonly string MusicMenuSound = "ost_menu";
        public static readonly string UiClickSound = "ui_click";
        public static readonly string UiUpgradeSound = "ui_upgrade";
        public static readonly string WinSound = "win";
        public static readonly string LooseSound = "loose";
        public static readonly string TakeMoney = "take_money";
        
        public static readonly string Buy = "buy";
        public static readonly string Take = "take";
        public static readonly string Place = "place";
        public static readonly string Money = "money";
        public static readonly string Error = "error";
        public static readonly string Signal = "signal";
        public static readonly string Button = "button";
        
        /*private static readonly List<string> ZombieHitSounds = new List<string>()
        {
            "zombie_hit_1",
            "zombie_hit_2",
            "zombie_hit_3",
            "zombie_hit_4",
            "zombie_hit_5",
            "zombie_hit_6",
        };
        
        private static readonly List<string> CarHitSounds = new List<string>()
        {
            "car_hit_1",
            "car_hit_2"
        };
        
        public static string GetRandomCarHit() => CarHitSounds[Random.Range(0, CarHitSounds.Count)];
        public static string GetRandomZombieHit() => ZombieHitSounds[Random.Range(0, ZombieHitSounds.Count)];*/
    }
}