using System;
using Microsoft.EntityFrameworkCore;

namespace HlidacStatu.Entities.Views;

[Keyless]
public class AdresyKVolbam : IEquatable<AdresyKVolbam>
{
    public int Id { get; set; }

    [FullTextSearch.Search]
    public string OneLiner { get; set; }

    public string Ulice { get; set; }
    public int? CisloDomovni { get; set; }
    public string Psc { get; set; }
    public string Obec { get; set; }

    public int TypOvm { get; set; }
    
    public string NazevUradu { get; set; }
    public string ObecUradu { get; set; }
    public string PscUradu { get; set; }
    public string UliceUradu { get; set; }
    public int? CisloDomovniUradu { get; set; }
    public string DatovkaUradu { get; set; }

    public bool Equals(AdresyKVolbam other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AdresyKVolbam)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}