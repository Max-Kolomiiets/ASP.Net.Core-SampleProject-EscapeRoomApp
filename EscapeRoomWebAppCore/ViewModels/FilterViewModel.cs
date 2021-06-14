using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(int? minCountPlayers = null, int? diffLevel = null, int? fearLevel = null)
        {
            MinCountPlayers = minCountPlayers;
            DifficultyLevel = diffLevel;
            FearLevel = fearLevel;
        }
        public SelectList DifficultyLevelList { get; private set; } = new SelectList(new string[] 
        {
            "all", "1", "2", "3", "4", "5"
        });
        public SelectList FearLevelList { get; private set; } = new SelectList(new string[]
        {
            "all", "1", "2", "3", "4", "5"
        });
        public int? MinCountPlayers { get; private set; }
        public int? DifficultyLevel { get; set; }
        public int? FearLevel { get; set; }

    }
}

//списка по сложности,
//количеству игроков, уровню страха.