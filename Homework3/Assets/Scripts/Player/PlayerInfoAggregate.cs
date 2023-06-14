using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class PlayerInfoAggregate
    {
        public UserInfo UserInfo { get; private set; }
        public CharacterInfo CharacterInfo { get; private set; }
        public PlayerLevel PlayerLevel { get; private set; }

        public PlayerInfoAggregate(UserInfo userInfo, CharacterInfo characterInfo, PlayerLevel playerLevel)
        {
            UserInfo = userInfo;
            CharacterInfo = characterInfo;
            PlayerLevel = playerLevel;
        }
    }
}