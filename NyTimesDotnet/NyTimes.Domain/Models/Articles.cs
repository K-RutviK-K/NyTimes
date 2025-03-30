using System;
using System.Collections.Generic;

namespace NyTimes.Domain.Models;

public partial class Articles
{
    public int Id { get; set; }

    public string? Section { get; set; }

    public string? Subsection { get; set; }

    public string? Title { get; set; }

    public string? Abstract { get; set; }

    public string? Url { get; set; }

    public string? Uri { get; set; }

    public string? Byline { get; set; }

    public string? ItemType { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? PublishedDate { get; set; }

    public string? MaterialTypeFacet { get; set; }

    public string? Kicker { get; set; }

    public string? ShortUrl { get; set; }

    public virtual ICollection<Facets> Facets { get; set; } = new List<Facets>();

    public virtual ICollection<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
}
