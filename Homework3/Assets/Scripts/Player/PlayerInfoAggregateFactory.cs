using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class PlayerInfoAggregateFactory: MonoBehaviour
    {
        [SerializeField] private Sprite _bobSprite;
        [SerializeField] private Sprite _aliceSprite;
        [SerializeField] private Sprite _joeSprite;

        public PlayerInfoAggregate Bob()
        {
            var characterInfo = new CharacterInfo();

            var stat1 = new CharacterStat("Strength", 10);
            characterInfo.AddStat(stat1);

            var stat2 = new CharacterStat("Agility", 15);
            characterInfo.AddStat(stat2);

            var stat3 = new CharacterStat("Speed", 10);
            characterInfo.AddStat(stat3);

            var stat4 = new CharacterStat("Regeneration", 5);
            characterInfo.AddStat(stat4);

            var stat5 = new CharacterStat("Luck", 12);
            characterInfo.AddStat(stat5);

            var userInfo = new UserInfo();
            userInfo.ChangeIcon(_joeSprite);
            userInfo.ChangeName("Bob");
            userInfo.ChangeDescription("Bob is a good guy");


            var playerLevel = new PlayerLevel();
            playerLevel.AddExperience(300);

            return new PlayerInfoAggregate(userInfo, characterInfo, playerLevel);
        }

        public PlayerInfoAggregate Alice()
        {
            var characterInfo = new CharacterInfo();

            var stat1 = new CharacterStat("Intelligence", 10);
            characterInfo.AddStat(stat1);

            var stat2 = new CharacterStat("Agility", 5);
            characterInfo.AddStat(stat2);

            var stat22 = new CharacterStat("Stamina", 9);
            characterInfo.AddStat(stat22);

            var stat3 = new CharacterStat("Speed", 8);
            characterInfo.AddStat(stat3);

            var stat4 = new CharacterStat("Regeneration", 3);
            characterInfo.AddStat(stat4);

            var stat5 = new CharacterStat("Luck", 8);
            characterInfo.AddStat(stat5);

            var userInfo = new UserInfo();
            userInfo.ChangeIcon(_aliceSprite);
            userInfo.ChangeName("Alice37");
            userInfo.ChangeDescription("Alice is a cool wizard. She commands magic.");

            var playerLevel = new PlayerLevel();
            playerLevel.SetLevel(35);
            playerLevel.AddExperience(750);

            return new PlayerInfoAggregate(userInfo, characterInfo, playerLevel);
        }

        public PlayerInfoAggregate Joe()
        {
            var characterInfo = new CharacterInfo();

            var stat1 = new CharacterStat("Stamina", 30);
            characterInfo.AddStat(stat1);

            var stat2 = new CharacterStat("Strength", 15);
            characterInfo.AddStat(stat2);

            var stat3 = new CharacterStat("Luck", 3);
            characterInfo.AddStat(stat3);

            var userInfo = new UserInfo();
            userInfo.ChangeIcon(_bobSprite);
            userInfo.ChangeName("MightyWarrior66");
            userInfo.ChangeDescription("Joe is a simple man. He runs, he attacks.");

            var playerLevel = new PlayerLevel();
            playerLevel.SetLevel(20);
            playerLevel.AddExperience(135);

            return new PlayerInfoAggregate(userInfo, characterInfo, playerLevel);
        }
    }
}