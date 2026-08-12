using JetBrains.Annotations;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Spt.Tables;

namespace SpeciallyWeightless;

[Injectable(TypePriority = OnLoadOrder.TraderRegistration - 1), UsedImplicitly]
public class SpeciallyWeightless(TemplateTable templateTable) : IOnLoad
{
    private static readonly MongoId SpecItemParent = new("5447e0e74bdc2d3c308b4567");
    
    public Task OnLoadAsync(CancellationToken token)
    {
        var itemsDb = templateTable.Items;
        
        foreach (var item in itemsDb.Where(item => item.Value.Parent == SpecItemParent))
        {
            var tpl = item.Value;
            if (tpl.Properties is { Weight: not null })
            {
                tpl.Properties.Weight = 0.0;
            }
        }
        return Task.CompletedTask;
    }
}