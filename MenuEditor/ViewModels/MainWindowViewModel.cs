using MenuEditor.Services;
using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MenuEditor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(IDataService dataService)
        {
            this.workPath = Configuration.GetValue<string>("finallyopen");
            this.dataService = dataService;

            this.TopMenu.SaveData += onSaveData;
            this.TopMenu.NewProject += onNewProj;

            if (System.IO.Directory.Exists(this.workPath))
            {
                MainWindowViewModel rawMenu = null;
                rawMenu = dataService.LoadData<MainWindowViewModel>(workPath + "/src.menu");
                load(rawMenu);
            }
        }

        [JsonIgnore]
        private IDataService dataService;

        [JsonIgnore]
        private string workPath;

        private void onSaveData()
        {
            if (!System.IO.Directory.Exists(workPath))
            {
                var Dialog = new System.Windows.Forms.FolderBrowserDialog();
                Dialog.Description = "文件夹必须为空";
                var result = Dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dialog.SelectedPath);
                    if (di.GetFiles().Length + di.GetDirectories().Length == 0)
                    {
                        //目录为空
                        this.workPath = Dialog.SelectedPath;
                        dataService.SaveData(workPath + "/src.menu", this);
                    }
                }
            }
            else
            {
                dataService.SaveData(workPath + "/src.menu", this);
            }
        }

        private void onNewProj()
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            Dialog.Description = "文件夹必须为空";
            var result = Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dialog.SelectedPath);
                if (di.GetFiles().Length + di.GetDirectories().Length == 0)
                {
                    //目录为空
                    this.workPath = Dialog.SelectedPath;
                }
            }
        }

        private void load(MainWindowViewModel rawMenu)
        {
            this.MenuCollection = rawMenu.MenuCollection;
            this.ModalCollection = rawMenu.ModalCollection;
        }

        [JsonIgnore]
        public TopMenuViewModel TopMenu = new TopMenuViewModel();

        private ObservableCollection<PageViewModel> _MenuCollection = new() { };
        [JsonProperty]
        public ObservableCollection<PageViewModel> MenuCollection
        {
            get => _MenuCollection;
            set
            {
                _ = SetProperty(ref _MenuCollection, value);
            }
        }

        private ObservableCollection<ModalDialogVewModel> _ModalCollection = new() { };
        [JsonProperty]
        public ObservableCollection<ModalDialogVewModel> ModalCollection
        {
            get => _ModalCollection;
            set
            {
                _ = SetProperty(ref _ModalCollection, value);
            }
        }
    }
}
