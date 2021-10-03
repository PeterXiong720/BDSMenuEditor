var System = dotnet.System;
var StreamWriter = System.IO.StreamWriter;
var Encoding = System.Text.Encoding;

var path = DIRECTORY.FullName + "\\fantasy_menu";

System.IO.Directory.CreateDirectory(path);

var Data = {
    Menus: JSON.parse(DATA.Menus),
    Modals: JSON.parse(DATA.Modals)
}
main(Data);

function getType(type) {
    const types = {
        cmd: "cmd",
        form: "form",
        code: "code",
        script: "script"
    }
    switch (type) {
        case 0:
            return types.cmd;
        case 1:
            return types.cmd;
        case 2:
            return types.cmd;
        case 3: //后台指令
            return types.code;
        case 4:
            return types.code;
        case 5:
            return types.script
        case 6:
            return types.form;
        case 7:
            return types.form;
    
        default:
            return types.cmd;
    }
}

function Button(model) {
    if (model.Image) {
        this.Image = model.Image;
    }
    if (model.ButtonExecutes) {
        if (model.ButtonExecutes.length > 0) {
            if (model.ButtonExecutes[0].Type == 2 || model.ButtonExecutes[0].Type == 7) {
                this.NeedOP = true;
            }

            this.onClick = {};
            this.onClick['Type'] = getType(model.ButtonExecutes[0].Type);
            if (model.ButtonExecutes[0].Type === 3) {
                this.onClick['Execute'] = "mc.runcmd(\'" + model.ButtonExecutes[0].Execute +"\')";
            } else {
                this.onClick['Execute'] = model.ButtonExecutes[0].Execute;
            }
            
        }
    }
}

function Menu(title, content, buttons) {
    if (title && content && title != "") {
        this.Title = title.trim();
        this.Content = content
        this.Buttons = new Array;
        if (buttons) {
            for (const btn of buttons) {
                if (btn.Text) {
                    this.Buttons.push(new Button(btn));
                }
            }
        }
    }
}

function ModalForm(model) {
    if (model.Title) {
        this.Title = model.Title;
    } else {
        this.Title = ""
    }

    if (model.Content) {
        this.Content = model.Content;
    } else {
        this.Content = "";
    }

    this.ButtonOne = ""
    if (model.ButtonOne)
        if (model.ButtonOne.Text) {
            this.ButtonOne = model.ButtonOne.Text;
        }
    this.ButtonTwo = "";
    if (model.ButtonTwo)
        if (model.ButtonTwo.Text) {
            this.ButtonTwo = model.ButtonTwo.Text;
        }

    this.ToString = () => {
        return "\"use strict\";exports.__esModule = true;exports.main = void 0;" +
            "function main(player, parent) {player.sendModalForm(\'" +
            this.Title + "\', \'" + this.Content + "\', \'" + this.ButtonOne + "\', \'" + this.ButtonTwo + "\', (pl, id) => {if (id == 1) {parent.SendForm(pl);}});}" +
            "exports.main = main;";
    };
}

function main(args) {
    for (let menu of args.Menus) {
        if (menu) {
            let _menu = new Menu(menu['Title'], menu['Content'], menu['Buttons']);
            let sw = new StreamWriter(path + "\\" + menu.FileName + ".json", false, Encoding.UTF8);
            sw.AutoFlush = true;
            sw.Write(JSON.stringify(_menu, undefined, 4));
            sw.Close();
            sw.Dispose();
        }
        
    }

    for (let modal of args.Modals) {
        if (modal) {
            let _modal = new ModalForm(modal);
            let sw = new StreamWriter(path + "\\" + modal.FileName + ".js", false, Encoding.UTF8);
            sw.AutoFlush = true;
            sw.Write(_modal.ToString());
            sw.Close();
            sw.Dispose();
        }
        
    }

    MessageBox.Show(
        "导出成功！",
        "导出完成",
        MessageBoxButton.OK,
        MessageBoxImage.Information);
    
    var p = new System.Diagnostics.Process();
    p.StartInfo.FileName = "explorer.exe";
    p.StartInfo.Arguments = path;
    p.Start();
}