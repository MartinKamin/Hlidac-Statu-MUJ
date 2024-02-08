using System;
using System.Linq;

namespace HlidacStatu.Entities.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PU_Organizace")]
public class PuOrganizace
{
    public const char PathSplittingChar = '>';
    
    [Key]
    public int Id { get; set; }

    public string Ico { get; set; }
    public string DS { get; set; }
    public string Nazev { get; set; }
    public string Info { get; set; }
    public string HiddenNote { get; set; }
    public string Zatrideni { get; set; }
    public string Oblast { get; set; }
    public string PodOblast { get; set; }

    // Navigation properties
    public virtual ICollection<PuOrganizaceTag> Tags { get; set; }
    public virtual ICollection<PuPlat> Platy { get; set; }
    public virtual ICollection<PuOrganizaceMetadata> Metadata { get; set; }
}