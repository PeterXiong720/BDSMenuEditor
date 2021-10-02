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
using System.Windows.Controls;

namespace MenuEditor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel() { }

        public MainWindowViewModel(IDataService dataService)
        {
            workPath = Configuration.GetValue<string>("finallyopen");
            this.dataService = dataService;

            TopMenu.OpenWorkSpace += onOpenWorkSpace;
            TopMenu.SaveData += onSaveData;
            TopMenu.NewProject += onNewProj;

            AddMenuCommand = new DelegateCommand<string>(new Action<string>(AddMenu));
            OpenDialogCommand = new DelegateCommand<string>(new Action<string>(OpenDialog));

            TopMenu.ExportCommand = new DelegateCommand(new Action(ExportMenu));
            SelectScriptDialogViewModal = TopMenu;

            if (System.IO.Directory.Exists(this.workPath))
            {
                MainWindowViewModel rawMenu = null;
                rawMenu = dataService.LoadData<MainWindowViewModel>(workPath + "/src.menu");
                Load(rawMenu);
            }
        }

        [JsonIgnore]
        private IDataService dataService;

        [JsonIgnore]
        private string workPath;

        private void onOpenWorkSpace()
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Dialog.SelectedPath);
                if (di.GetFiles().Length == 0)
                {
                    //目录为空
                }
                foreach (var item in di.GetFiles())
                {
                    if (item.Name == "src.menu")
                    {
                        workPath = Dialog.SelectedPath;
                        MainWindowViewModel rawMenu = null;
                        rawMenu = dataService.LoadData<MainWindowViewModel>(item.FullName);
                        Load(rawMenu);

                        _ = Configuration.SaveValue("finallyopen", workPath);

                        break;
                    }
                }
            }
        }

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
                        _ = dataService.SaveData(workPath + "/src.menu", this);
                    }
                }
            }
            else
            {
                _ = dataService.SaveData(workPath + "/src.menu", this);
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
                    _ = dataService.SaveData(workPath + "/src.menu", this);
                }
            }
        }

        private void Load(MainWindowViewModel rawMenu)
        {
            if (rawMenu == null)
            {
                rawMenu = new MainWindowViewModel();
            }
            MenuCollection = rawMenu.MenuCollection;
            ModalCollection = rawMenu.ModalCollection;

            foreach (var item in MenuCollection)
            {
                var vmodel = item;
                item.EditMenu = new Views.EditMenu(ref vmodel);
            }

            foreach (var item in ModalCollection)
            {
                var vmodel = item;
                item.EditModalDialog = new Views.EditModalDialog(ref vmodel);
            }
        }

        private void ExportMenu()
        {
            if (TopMenu.SelectScript != null)
            {
                var ExportService = new ExportMenuService(
                    TopMenu.SelectScript,
                    workPath,
                    TopMenu.ScriptDebug);
                TopMenu.ProgressBarVisibility = System.Windows.Visibility.Visible;
                ExportService.ExportMenu(this);
                TopMenu.ProgressBarVisibility = System.Windows.Visibility.Hidden;
            }

        }

        private PageViewModel currentEditMenu;
        [JsonIgnore]
        public PageViewModel CurrentEditMenu
        {
            get => currentEditMenu;
            set
            {
                _ = SetProperty(ref currentEditMenu, value);
                if (value != null)
                {
                    EditSpace = value.EditMenu;
                }
            }
        }

        private ModalDialogVewModel currentEditModal;
        [JsonIgnore]
        public ModalDialogVewModel CurrentEditModal
        {
            get => currentEditModal;
            set
            {
                _ = SetProperty(ref currentEditModal, value);
                if (value != null)
                {
                    EditSpace = value.EditModalDialog;
                }
            }
        }

        private UserControl editSpace;
        [JsonIgnore]
        public UserControl EditSpace
        {
            get => editSpace;
            set => SetProperty(ref editSpace, value);
        }

        [JsonIgnore]
        public TopMenuViewModel TopMenu = new TopMenuViewModel();

        private ObservableCollection<PageViewModel> _MenuCollection = new() { };
        [JsonProperty]
        public ObservableCollection<PageViewModel> MenuCollection
        {
            get => _MenuCollection;
            set => SetProperty(ref _MenuCollection, value);
        }

        private ObservableCollection<ModalDialogVewModel> _ModalCollection = new() { };
        [JsonProperty]
        public ObservableCollection<ModalDialogVewModel> ModalCollection
        {
            get => _ModalCollection;
            set => SetProperty(ref _ModalCollection, value);
        }

        private string addType;
        [JsonIgnore]
        public string AddItemType
        {
            get => addType;
            set => SetProperty(ref addType, value.Trim());
        }

        private string addName;
        [JsonIgnore]
        public string AddItemName
        {
            get => addName;
            set => SetProperty(ref addName, value.Trim());
        }

        [JsonIgnore]
        public AddItemDialogViewModel DialogViewModel { get; set; } = new() { };
        [JsonIgnore]
        public TopMenuViewModel SelectScriptDialogViewModal { get; set; }

        [JsonIgnore]
        public DelegateCommand<string> AddMenuCommand { get; set; }
        private void AddMenu(string type)
        {
            if (type == "menu")
            {
                var menu = new PageViewModel()
                {
                    FileName = AddItemName,
                    Title = "",
                    Content = ""
                };
                menu.EditMenu = new Views.EditMenu(ref menu);
                MenuCollection.Add(menu);
            }
            else if (type == "modal")
            {
                var menu = new ModalDialogVewModel()
                {
                    FileName = AddItemName,
                    Title = "",
                    Content = ""
                };
                menu.EditModalDialog = new Views.EditModalDialog(ref menu);
                ModalCollection.Add(menu);
            }

            DialogViewModel.IsDialogOpen = false;
            AddItemName = "";
        }

        [JsonIgnore]
        public DelegateCommand<string> OpenDialogCommand { get; set; }
        private void OpenDialog(string arg)
        {
            AddItemType = arg;
            DialogViewModel.DialogContent = new Views.AddItemDialog();
            DialogViewModel.IsDialogOpen = true;
        }
    }
}
