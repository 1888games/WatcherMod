using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Timeline.Epochs;

public class Watcher3Epoch : EpochModel
{
    public static readonly List<CardModel> Cards =
    [
        ModelDb.Card<MoltenFist>(),
        ModelDb.Card<Cruelty>(),
        ModelDb.Card<Dominate>()
    ];

    public override string Id => "WATCHER3_EPOCH";

    public override EpochEra Era => EpochEra.Blight1;

    public override int EraPosition => 2;

    public override string StoryId => "Watcher";

    public override bool IsArtPlaceholder => false;

    public override string UnlockText => CreateCardUnlockText(Cards);

    public override void QueueUnlocks()
    {
        //NTimelineScreen.Instance.QueueCardUnlock(Cards);
    }
}