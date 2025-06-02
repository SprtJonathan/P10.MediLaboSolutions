using System.ComponentModel.DataAnnotations;

namespace MediLaboSolutions.Common.Enumerables;

/// <summary>
/// Niveau de risque du patient
/// </summary>
public enum ENiveauRisque
{
    [Display(Name = "Aucun risque ✅")]
    None,

    [Display(Name = "Risque limité ⚠️")]
    Borderline,

    [Display(Name = "Danger ❗")]
    InDanger,

    [Display(Name = "Apparition précoce 🚨")]
    EarlyOnset
}
