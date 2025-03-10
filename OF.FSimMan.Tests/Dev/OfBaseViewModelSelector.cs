using OF.Base.ViewModel;

namespace OF.FSimMan.Tests.Dev
{
    [TestClass]
    public sealed class OfBaseViewModelSelector
    {
        [TestMethod]
        public void TestAllPossibilities()
        {
            ViewModelSelector vms = new ViewModelSelector();
            Assert.AreEqual(0, vms.OpenViewModels.Count);
            Assert.AreEqual(null, vms.CurrentViewModel);

            // Open new pvm
            PersistantViewModel pvm = new PersistantViewModel();
            vms.OpenViewModel(pvm);
            Assert.AreEqual(1, vms.OpenViewModels.Count);
            Assert.AreEqual(pvm, vms.CurrentViewModel);

            // Try close pvm
            vms.CloseViewModel(pvm);
            Assert.AreEqual(1, vms.OpenViewModels.Count);
            Assert.AreEqual(pvm, vms.CurrentViewModel);

            // Open new nvm
            NonPersistantViewModel nvm = new NonPersistantViewModel();
            vms.OpenViewModel(nvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(nvm, vms.CurrentViewModel);

            // Close nvm | Current
            vms.CloseCurrentViewModel();
            Assert.AreEqual(1, vms.OpenViewModels.Count);
            Assert.AreEqual(pvm, vms.CurrentViewModel);

            // Open nvm
            vms.OpenViewModel(nvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(nvm, vms.CurrentViewModel);

            // Open new avm
            AutocloseableViewModel avm = new AutocloseableViewModel();
            vms.OpenViewModel(avm);
            Assert.AreEqual(3, vms.OpenViewModels.Count);
            Assert.AreEqual(avm, vms.CurrentViewModel);

            // Reopen nvm
            vms.OpenViewModel(nvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(nvm, vms.CurrentViewModel);

            // Reopen avm
            vms.OpenViewModel(avm);
            Assert.AreEqual(3, vms.OpenViewModels.Count);
            Assert.AreEqual(avm, vms.CurrentViewModel);

            // Try close avm through autoclose
            avm.ExecutePreventAutoclose(() => vms.OpenViewModel(nvm));
            Assert.AreEqual(3, vms.OpenViewModels.Count);
            Assert.AreEqual(nvm, vms.CurrentViewModel);

            // Close avm through autoclose
            vms.OpenViewModel(pvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(pvm, vms.CurrentViewModel);
        }

        private class PersistantViewModel : ViewModelBase
        {
            public PersistantViewModel() : base("Persistant", true) { }
        }

        private class NonPersistantViewModel : ViewModelBase
        {
            public NonPersistantViewModel() : base("NonPersistant", false, false) { }
        }

        private class AutocloseableViewModel : ViewModelBase
        {
            public AutocloseableViewModel() : base("Autocloseable", false, true) { }
        }
    }
}
