using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DEF_PARAMS;

namespace CD_VISION_DIALOG
{

    public partial class FormBaseRecpCopy : Form
    {
        public delegate void dele_GenerateBaseRecp(string temp);
        public event dele_GenerateBaseRecp event_GeneratedBaseRecp;

        public string strBasePath { get; set; }

        public FormBaseRecpCopy()
        {
             InitializeComponent();
        }

        private void BTN_GENERATE_Click(object sender, EventArgs e)
        {
            string strBase = TXT_BASE_FILE.Text;

            TXT_INHERITED_TARGET.Text = TXT_INHERITED_TARGET.Text.ToUpper();
            string strTarget = TXT_INHERITED_TARGET.Text;

            //*************************************************************************************
            // Exception 1 : Empty
            if (strTarget == string.Empty){ MessageBox.Show("Invalid Target Namming.\n Please Check Target Name.\n→"+strTarget, "Invalid Target Naming", MessageBoxButtons.OK, MessageBoxIcon.Error);return;}


            //*************************************************************************************
            // Exception 2 : Seperator Error

            bool bValidity = false;
            int nTargetLength = strTarget.Length;
            string strTail = strTarget.Substring(nTargetLength - 4).ToUpper();

            if (strTail == "_ADI" || strTail == "_ACI") bValidity = true;

            if (bValidity == false)
            {
                MessageBox.Show("Target Name Has To Finished {_ADI} or {_ACI}", "Namming Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //*************************************************************************************
            // Enforce Uppercase 
            string strFinal = Path.Combine(this.strBasePath, strTarget + ".INI").ToUpper();

            string msg = string.Empty;

             //*************************************************************************************
            // Excpetions 2 : Duplication

            bool bProceed = true;

            if (File.Exists(strFinal) == true)
            {
                if (MessageBox.Show("Already Target File Exist.\nDo You Want To Overwrite?", "File Duplication", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bProceed = true;
                }
                else
                {
                    bProceed = false;
                }
            }

            if (bProceed == true)
            {
                try
                {
                    File.Delete(strFinal);
                    string strSourceFile = Path.Combine(this.strBasePath, TXT_BASE_FILE.Text + ".INI");
                    File.Copy(strSourceFile, strFinal);

                    msg = string.Format("[PASS] Base Recipe Has Created.\n" + strFinal);
                }
                catch (Exception ex)
                {
                    msg = ex.ToString();
                }

                // triggered twice !!! 
                event_GeneratedBaseRecp(msg);
                this.Close();
            }
        }

        private void BaseRecpCopyForm_Load(object sender, EventArgs e)
        {
        }

        public void SetBaseFilePath(string strBasePath)
        {
            this.strBasePath = strBasePath;
        }
        public void _TriggerSetBaseRecp(string strBaseRecp)
        {
             TXT_BASE_FILE.Text = strBaseRecp.Replace(".INI", "");;
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
