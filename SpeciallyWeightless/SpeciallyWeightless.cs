using JetBrains.Annotations;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Servers;

namespace SpeciallyWeightless;

[Injectable(TypePriority = OnLoadOrder.TraderRegistration - 1), UsedImplicitly]
public class SpeciallyWeightless(DatabaseServer databaseServer) : IOnLoad
{
    private MongoId _specItemParent = new("5447e0e74bdc2d3c308b4567");
    
    public Task OnLoad()
    {
        var itemsDb = databaseServer.GetTables().Templates.Items;
        foreach (var item in itemsDb.Where(item => item.Value.Parent == _specItemParent))
        {
            var tpl = item.Value;
            if (tpl.Properties != null && tpl.Properties.Weight != null)
            {
                tpl.Properties.Weight = 0.0;
            }
        }
        return Task.CompletedTask;
    }
}