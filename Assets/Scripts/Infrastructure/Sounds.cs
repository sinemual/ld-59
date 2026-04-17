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
        public static readonly string ExplosionSound = "explosion";
        public static readonly string FlamethrowerSound = "flamethrower";
        public static readonly string MachineGunShootSound = "machine_gun_shoot";
        public static readonly string RocketLauncherSound = "rocket_launch";
        public static readonly string BulletHitSound = "bullet_hit";
        public static readonly string DeathSound = "death";
        public static readonly string ProduceWorkSound = "produce_work";
        public static readonly string HoleShh = "hole_shh";
        public static readonly string CarSound = "car";
        public static readonly string CarStartSound = "car_start";
        public static readonly string MoneySound = "money";
        public static readonly string BuySound = "buy";
        public static readonly string UnpackSound = "unpack";
        public static readonly string WinSound = "win";
        public static readonly string LooseSound = "loose";
        public static readonly string MachineGunSuperShootSound = "machine_gun_super_shoot";
        public static readonly string CarIncreaseSpeedSound = "car_increase_speed";
        public static readonly string CarDecreaseSpeedSound = "car_decrease_speed";
        public static readonly string LevelExit = "level_exit";
        public static readonly string Lamp = "lamp";
        public static readonly string ExperimentComplete = "experiment_complete";
        public static readonly string ExperimentStart = "experiment_start";
        public static readonly string TakeMoney = "take_money";
        public static readonly string Step1 = "step_1";
        public static readonly string Step2 = "step_2";
        public static readonly string Ladle = "ladle";
        
        private static readonly List<string> ZombieHitSounds = new List<string>()
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
        public static string GetRandomZombieHit() => ZombieHitSounds[Random.Range(0, ZombieHitSounds.Count)];
    }
}