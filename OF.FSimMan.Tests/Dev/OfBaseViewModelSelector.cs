using OF.Base.ViewModel;
using OF.FSimMan.ViewModel;

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

            // Open persistant VM
            MainViewModel mvm = new MainViewModel();
            vms.OpenViewModel(mvm);
            Assert.AreEqual(1, vms.OpenViewModels.Count);
            Assert.AreEqual(mvm, vms.CurrentViewModel);

            // Close current (persistant) VM
            vms.CloseCurrentViewModel();
            Assert.AreEqual(1, vms.OpenViewModels.Count);
            Assert.AreEqual(mvm, vms.CurrentViewModel);

            // Open persistant VM2
            HomeViewModel hvm = new HomeViewModel();
            vms.OpenViewModel(hvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(hvm, vms.CurrentViewModel);

            // Close persistant VM2
            vms.CloseViewModel(hvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(hvm, vms.CurrentViewModel);

            // Reopen persistant VM
            vms.OpenViewModel(mvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(mvm, vms.CurrentViewModel);

            // Open autocloseable VM
            SettingsViewModel svm = new SettingsViewModel();
            vms.OpenViewModel(svm);
            Assert.AreEqual(3, vms.OpenViewModels.Count);
            Assert.AreEqual(svm, vms.CurrentViewModel);

            // Close autocloseable VM
            vms.CloseCurrentViewModel();
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(hvm, vms.CurrentViewModel);

            // Open autocloseable VM
            vms.OpenViewModel(svm);
            Assert.AreEqual(3, vms.OpenViewModels.Count);
            Assert.AreEqual(svm, vms.CurrentViewModel);

            // Reopen persistant VM => autocloses autocloseable VM
            vms.OpenViewModel(mvm);
            Assert.AreEqual(2, vms.OpenViewModels.Count);
            Assert.AreEqual(mvm, vms.CurrentViewModel);
        }
    }
}
