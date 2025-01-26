namespace Lynx.InazumaEleven.Logic
{
    /// <summary>
    /// Represents a character base interface with properties for character attributes.
    /// </summary>
    public interface ICharabase
    {
        /// <summary>
        /// Gets or sets the base ID of the character.
        /// </summary>
        int BaseHash { get; set; }

        /// <summary>
        /// Gets or sets the model number of the character.
        /// </summary>
        int ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the name ID of the character.
        /// </summary>
        int NameHash { get; set; }

        /// <summary>
        /// Gets or sets the nickname ID of the character.
        /// </summary>
        int NicknameHash { get; set; }

        /// <summary>
        /// <para>Gets or sets the character base type, which can be one of the following: </para>
        /// 0 = Unknown<br />
        /// 1 = Player<br />
        /// 2 = Unused<br />
        /// 3 = NPC<br />
        /// 4 = NPC Other<br />
        /// </summary>
        int CharaBaseType { get; set; }

        /// <summary>
        /// Gets or sets the body type of the character.
        /// </summary>
        int Body { get; set; }

        /// <summary>
        /// Gets or sets the skin type of the character.
        /// </summary>
        int Skin { get; set; }

        /// <summary>
        /// Gets or sets the gender of the character.
        /// </summary>
        int Gender { get; set; }

        /// <summary>
        /// Gets or sets the year of the character.
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// Gets or sets the description ID of the character.
        /// </summary>
        int DescriptionHash { get; set; }
    }
}
