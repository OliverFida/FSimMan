using OF.Base.Objects;

namespace OF.Base.Tests.TestUtility
{
    public class AnyEditor : EditorBase<AnyEditableObject>
    {
        public AnyEditor() : base(new AnyEditableObject())
        {

        }

        public void TriggerBeginEdit() => BeginEdit();
        public void TriggerCancelEdit() => CancelEdit();
        public void TriggerEndEdit() => EndEdit();
    }
}
