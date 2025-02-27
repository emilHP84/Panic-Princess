using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Dialogues")]
public class DialogueList
{
    [XmlElement("Dialogue")]
    public List<Dialogue> Dialogues { get; set; }
}