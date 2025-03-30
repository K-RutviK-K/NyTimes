using System;
using System.Collections.Generic;

namespace NyTimes.Domain.Models;

public partial class Facets
{
    public int Id { get; set; }

    public int? ArticleId { get; set; }

    public string? FacetType { get; set; }

    public string? FacetValue { get; set; }

    public virtual Articles? Article { get; set; }
}
