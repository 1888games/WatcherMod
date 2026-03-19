using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Timeline.Epochs;

public class Watcher7Epoch : EpochModel
{
    public static readonly List<CardModel> Cards =
    [
        ModelDb.Card<MoltenFist>(),
        ModelDb.Card<Cruelty>(),
        ModelDb.Card<Dominate>()
    ];

    public override string Id => "WATCHER7_EPOCH";

    public override EpochEra Era => EpochEra.Blight1;

    public override int EraPosition => 6;

    public override string StoryId => "Watcher";

    public override bool IsArtPlaceholder => false;

    public override string UnlockText => CreateCardUnlockText(Cards);

    public override void QueueUnlocks()
    {
        //NTimelineScreen.Instance.QueueCardUnlock(Cards);
    }
}