using System;
using System.Collections.Generic;

namespace BitsEFClasses.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public int? Version { get; set; }

    public DateTime? Date { get; set; }

    public int StyleId { get; set; }

    public decimal Volume { get; set; }

    public string Brewer { get; set; } = null!;

    public decimal? BoilTime { get; set; }

    public decimal? BoilVolume { get; set; }

    public decimal? Efficiency { get; set; }

    public int? FermentationStages { get; set; }

    public decimal? EstimatedOg { get; set; }

    public decimal? EstimatedFg { get; set; }

    public string? EstimatedColor { get; set; }

    public decimal? EstimatedAbv { get; set; }

    public decimal? ActualEfficiency { get; set; }

    public int? EquipmentId { get; set; }

    public string? CarbonationUsed { get; set; }

    public sbyte? ForcedCarbonation { get; set; }

    public int? KegPrimingFactor { get; set; }

    public decimal? CarbonationTemp { get; set; }

    public int? MashId { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual Equipment? Equipment { get; set; }

    public virtual Mash? Mash { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual Style Style { get; set; } = null!;
}
