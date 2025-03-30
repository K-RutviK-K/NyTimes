using System;
using System.Collections.Generic;

namespace NyTimes.Domain.Models;

public partial class Multimedia
{
    public int Id { get; set; }

    public int? ArticleId { get; set; }

    public string? Url { get; set; }

    public string? Format { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public string? Type { get; set; }

    public string? Subtype { get; set; }

    public string? Caption { get; set; }

    public string? Copyright { get; set; }

    public virtual Articles? Article { get; set; }
}
