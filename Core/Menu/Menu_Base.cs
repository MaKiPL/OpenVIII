﻿using System.Linq;

namespace OpenVIII
{
    /// <summary>
    /// Root class all menu objects can grow from.
    /// </summary>
    public abstract class Menu_Base
    {
        #region Properties

        /// <summary>
        /// If enabled the menu is visable and all functionality works. Else everything is hidden and
        /// nothing functions.
        /// </summary>
        public bool Enabled { get; private set; } = true;

        /// <summary>
        /// Character who has the junctions and inventory. Same as VisableCharacter unless TeamLaguna.
        /// </summary>
        public Characters Character { get; protected set; } = Characters.Blank;

        /// <summary>
        /// Required to support Laguna's Party. They have unique stats but share junctions and inventory.
        /// </summary>
        public Characters VisableCharacter { get; protected set; } = Characters.Blank;

        /// <summary>
        /// Position of party member 0,1,2. If -1 at the time of setting the character wasn't in the party.
        /// </summary>
        public sbyte PartyPos { get; protected set; }

        #endregion Properties

        #region Methods

        public abstract void Draw();

        /// <summary>
        /// Hide object prevents drawing, update, inputs.
        /// </summary>
        public virtual void Hide() => Enabled = false;

        public abstract bool Inputs();

        /// <summary>
        /// Things that change rarely. Like a party member changes or Laguna dream happens.
        /// </summary>
        public virtual void Refresh() => RefreshChild();

        /// <summary>
        /// For child items.
        /// </summary>
        protected virtual void RefreshChild()
        {
        }

        /// <summary>
        /// Update set characters and then refresh.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="visablecharacter"></param>
        public virtual void Refresh(Characters character, Characters? visablecharacter = null)
        {
            if ((character != Character || (visablecharacter ?? character) != VisableCharacter) && character != Characters.Blank && visablecharacter != Characters.Blank)
            {
                Character = character;
                VisableCharacter = visablecharacter ?? character;
                PartyPos = (sbyte)(Memory.State?.PartyData?.Where(x => !x.Equals(Characters.Blank)).ToList().FindIndex(x => x.Equals(Character)) ?? -1);
            }
            Refresh();
        }

        /// <summary>
        /// Show object enables drawing, update, inputs.
        /// </summary>
        public virtual void Show() => Enabled = true;

        public abstract bool Update();

        protected abstract void Init();

        #endregion Methods
    }
}