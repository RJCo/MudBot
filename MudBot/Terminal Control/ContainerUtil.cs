/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: ContainerUtil.cs,v 1.2 2005/04/20 08:45:44 okajima Exp $
*/
using Poderosa.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace Poderosa
{
    internal enum LogFileCheckResult
    {
        Create,
        Append,
        Cancel,
        Error
    }

    internal class GCUtil : GUtil
    {
        //既存のファイルであったり、書き込み不可能だったら警告する
        public static LogFileCheckResult CheckLogFileName(string path, Form parent)
        {
            try
            {
                if (path.Length == 0)
                {
                    GUtil.Warning(parent, "The path is not specified.");
                    return LogFileCheckResult.Cancel;
                }

                if (File.Exists(path))
                {
                    if ((FileAttributes.ReadOnly & File.GetAttributes(path)) != 0)
                    {
                        Warning(parent, $"{path} is not writable.");
                        return LogFileCheckResult.Cancel;
                    }

                    ThreeButtonMessageBox mb = new ThreeButtonMessageBox
                    {
                        Message = $"The file {path} already exists.",
                        Text = "Log File",
                        YesButtonText = "Overwrite",
                        NoButtonText = "Append",
                        CancelButtonText = "Cancel"
                    };
                    switch (ShowModalDialog(parent, mb))
                    {
                        case DialogResult.Cancel:
                            return LogFileCheckResult.Cancel;
                        case DialogResult.Yes: //上書き
                            return LogFileCheckResult.Create;
                        case DialogResult.No:  //追記
                            return LogFileCheckResult.Append;
                        default:
                            break;
                    }
                }

                return LogFileCheckResult.Create;

            }
            catch (Exception ex)
            {
                Warning(parent, ex.Message);
                return LogFileCheckResult.Error;
            }
        }

        //ダイアログでログファイルを開く
        public static string SelectLogFileByDialog(Form parent)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "log",
                Title = "Save Log File",
                Filter = "Log Files(*.log)|*.log|All Files(*.*)|*.*"
            };

            if (ShowModalDialog(parent, dlg) == DialogResult.OK)
            {
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }
        public static string SelectPrivateKeyFileByDialog(Form parent)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = GApp.Options.DefaultKeyDir,
                Title = "Select Private Key File",
                Filter = "Key Files(*.bin;*)|*.bin;*"
            };

            if (ShowModalDialog(parent, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultKeyDir = FileToDir(dlg.FileName);
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }
        public static string SelectPictureFileByDialog(Form parent)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = GApp.Options.DefaultFileDir,
                Title = "Select Picture File",
                Filter = "Picture Files(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png"
            };
            if (ShowModalDialog(parent, dlg) == DialogResult.OK)
            {
                GApp.Options.DefaultFileDir = FileToDir(dlg.FileName);
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }

        //.NET1.1SP1 対策で、ダイアログ表示の手続きにひとくせあり
        public static DialogResult ShowModalDialog(Form parent, Form dialog)
        {
            DialogResult r;
            if (parent.Modal)
            {
                if (GApp.Options.HideDialogForSP1Issue)
                {
                    parent.Visible = false;
                    r = dialog.ShowDialog(parent);
                    parent.Visible = true;
                }
                else
                {
                    parent.Enabled = false;
                    r = dialog.ShowDialog(parent);
                    parent.Enabled = true;
                }
            }
            else
            {
                parent.Enabled = false;
                r = dialog.ShowDialog(parent);
                parent.Enabled = true;
            }

            dialog.Dispose();
            return r;
        }
        //ShowDialogを使わずにそれっぽく見せる
        public static void ShowPseudoModalDialog(Form parent, Form dialog)
        {
            dialog.Owner = GApp.Frame;
            //centering
            dialog.Left = parent.Left + parent.Width / 2 - dialog.Width / 2;
            dialog.Top = parent.Top + parent.Height / 2 - dialog.Height / 2;

            parent.Enabled = false;
            dialog.Show();
        }
        public static DialogResult ShowModalDialog(Form parent, CommonDialog dialog)
        {
            parent.Enabled = false;
            DialogResult r = dialog.ShowDialog();
            parent.Enabled = true;
            dialog.Dispose();
            return r;
        }

    }

}
