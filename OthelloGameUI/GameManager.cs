using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OthelloGame;

namespace OthelloGameUI
{
    public class GameManager
    {        
        private OthelloGameForm m_GameOnForm;
        private OthelloGameSetting m_FormSettings;

        //Get Set 
        public OthelloGameForm GameOnForm
        {
            get
            {
                return m_GameOnForm;
            }
            set
            {
                m_GameOnForm= value;
            }
        }

        public OthelloGameSetting FormSettings
        {
            get
            {
                return m_FormSettings;
            }
            set
            {
                m_FormSettings = value;
            }
        }

        //Ctor
        public GameManager()
        {
            m_FormSettings = new OthelloGameSetting();
            m_FormSettings.ShowDialog();
            GameOnForm = new OthelloGameForm(m_FormSettings.IsTwoPlayers, m_FormSettings.BoardSize);
            GameOnForm.ShowDialog(); 
        }

    }
}
