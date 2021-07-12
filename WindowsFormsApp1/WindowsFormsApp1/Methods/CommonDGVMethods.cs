using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Methods {
    class CommonDGVMethods {

        public void SetDataGridViewProperties(DataGridView dataGridView) {
            dataGridView.MultiSelect = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void MakeColumnsDataGridView(DataGridView dataGridView, bool hasDeleteButton, int columnsCount, List<string> columnsNames, List<string> hideColumnsNames, Action<object, DataGridViewCellEventArgs> CellClick) {
            dataGridView.ColumnCount = columnsCount;
            int i = 0;
            if (hasDeleteButton == true) {
                DataGridViewButtonColumn DeleteButton = new DataGridViewButtonColumn();
                DeleteButton.Name = "Delete";
                DeleteButton.Text = "Delete";
                DeleteButton.UseColumnTextForButtonValue = true;
                if (dataGridView.Columns["Delete"] == null) {
                    dataGridView.Columns.Insert(0, DeleteButton);
                    
                    dataGridView.CellClick += new DataGridViewCellEventHandler(CellClick);
                }
                i++;
            }
            foreach (string name in columnsNames) {
                dataGridView.Columns[i].Name = name;
                i++;
            }
            foreach (string name in hideColumnsNames) {
                dataGridView.Columns[name].Visible = false;
            }

            if (dataGridView.Columns.Contains("") == true) {
                dataGridView.Columns.Remove("");
            }
        }
    }
}
