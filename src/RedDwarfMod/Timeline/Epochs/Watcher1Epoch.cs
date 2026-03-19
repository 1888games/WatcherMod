using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Timeline;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using RedDwarfMod.Models.Characters;

namespace RedDwarfMod.Timeline.Epochs;

public class Watcher1Epoch : EpochModel
{
    public override string Id => "WATCHER1_EPOCH";

    public override EpochEra Era => EpochEra.Invitation5;

    public override int EraPosition => 0;

    public override string StoryId => "Watcher";

    public override bool IsArtPlaceholder => false;

    public override EpochModel[] GetTimelineExpansion()
    {
        return
        [
            Get(GetId<Watcher2Epoch>()),
            Get(GetId<Watcher3Epoch>()),
            Get(GetId<Watcher4Epoch>()),
            Get(GetId<Watcher5Epoch>()),
            Get(GetId<Watcher6Epoch>()),
            Get(GetId<Watcher7Epoch>())
        ];
    }

    public override void QueueUnlocks()
    {
        NTimelineScreen.Instance.QueueCharacterUnlock<Watcher>(this);
        SaveManager.Instance.Progress.PendingCharacterUnlock = ModelDb.Character<Watcher>().Id;
        QueueTimelineExpansion(GetTimelineExpansion());
    }
}