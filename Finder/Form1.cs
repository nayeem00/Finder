using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
namespace Finder
{
    public partial class MainForm : Form 
    {
        public delegate void workAfterDelay();
        Panel CurrentVisiblePanel;
        String selectedArea;
        String selectedCategory;
        LdatabaseDataContext dctx = new LdatabaseDataContext();
        int selecteditemID;
        bool isMouseDown = false;
        Point downPoint;
        
        public MainForm()
        {
            InitializeComponent();
            this.panelToggle(Welcome_panel,Welcome_panel);
            this.delayedWork(new workAfterDelay(afterStartWait));

            //add animation
            pictureBox1.Image = Image.FromFile("start.gif");
            endPictureBox.Image = Image.FromFile("end.gif");

            this.labelTransparent();
            this.refreshlist();
            
            

            



         }

        public void refreshlist()
        {
            AreaComboBox.Items.Clear();
            this.setComboboxItem(AreaComboBox, "area");
            Category_combo_box.Items.Clear();
            this.setComboboxItem(Category_combo_box, "category");
            item_add_area_box.Items.Clear();
            this.setComboboxItem(item_add_area_box, "area");
            item_add_category_box.Items.Clear();
            this.setComboboxItem(item_add_category_box, "category");
            item_modify_area_box.Items.Clear();
            this.setComboboxItem(item_modify_area_box, "area");
            item_modify_category_box.Items.Clear();
            this.setComboboxItem(item_modify_category_box, "category");
            Category_combo_box_for_modify.Items.Clear();
            this.setComboboxItem(Category_combo_box_for_modify, "category");
            Category_combo_box_for_delete.Items.Clear();
            this.setComboboxItem(Category_combo_box_for_delete, "category");

            Area_combobox_fo_modify.Items.Clear();
            this.setComboboxItem(Area_combobox_fo_modify, "area");
            Area_combobox_fo_delete.Items.Clear();
            this.setComboboxItem(Area_combobox_fo_delete, "area");
        }
        public void setComboboxItem(ComboBox temp, String s)
        {
            
            if(s=="area")
            {
                for (int i = 0; i < dctx.AreaTables.Count(); i++)
                {
                
                    AreaTable a = dctx.AreaTables.SingleOrDefault(X => X.Id == i + 1);
                    temp.Items.Add(a.Name);
                }
            }
            else if(s=="category")
            {
                for (int i = 0; i < dctx.CategoryTables.Count(); i++)
                {

                    CategoryTable c = dctx.CategoryTables.SingleOrDefault(X => X.Id == i + 1);
                    temp.Items.Add(c.Name);
                }
            }
            
            
        }
        public void labelTransparent()
        {
            
            //transparent label
            ExitLabel.BackColor = Color.Transparent;
            Admin_panel_label.BackColor = Color.Transparent;
            Result_found_label.BackColor = Color.Transparent;
            back_area_label.BackColor = Color.Transparent;
            Result_show_panel.BackColor = Color.Transparent;
            back_area_label.BackColor = Color.Transparent;
            distance_show_label.BackColor = Color.Transparent;
            distance_label.BackColor = Color.Transparent;
            area_show_label.BackColor = Color.Transparent;
            Area_label.BackColor = Color.Transparent;
            Adress_show_label.BackColor = Color.Transparent;
            Adress_label.BackColor = Color.Transparent;
            PN_show_label.BackColor = Color.Transparent;
            PN_label.BackColor = Color.Transparent;
            name_show_label.BackColor = Color.Transparent;
            name_label.BackColor = Color.Transparent;
            Back_resultPnael_label.BackColor = Color.Transparent;
            Logout_label.BackColor = Color.Transparent;
            welcome_label.BackColor = Color.Transparent;
            select_what_label.BackColor = Color.Transparent;
            Item_edit_label.BackColor = Color.Transparent;
            backTo_Item_edit_panel_label.BackColor = Color.Transparent;
            Category_name_label_for_Mdify.BackColor = Color.Transparent;
            Category_name_label_for_add.BackColor = Color.Transparent;
            Area_name_label_for_modify.BackColor = Color.Transparent;
            Area_name_label_for_add.BackColor = Color.Transparent;

            Back_To_previous.BackColor = Color.Transparent;
            item_add_name_label.BackColor = Color.Transparent;
            item_add_adress_label.BackColor = Color.Transparent;
            item_add_pn_label.BackColor = Color.Transparent;
            item_delete_id_label.BackColor = Color.Transparent;
            item_delete_adress_label.BackColor = Color.Transparent;
            item_delete_pn_label.BackColor = Color.Transparent;
            item_delete_name_label.BackColor = Color.Transparent;
            item_modify_id_label.BackColor = Color.Transparent;
            item_modify_adress_label.BackColor = Color.Transparent;
            item_modify_pn_label.BackColor = Color.Transparent;
            item_modify_name_label.BackColor = Color.Transparent;

        }


        private async Task delayedWork(workAfterDelay w)
        {
            await Task.Delay(1000);
            w();
            
            
         }

        public void afterStartWait()
        {
            this.panelToggle(Welcome_panel, Select_area_panel);
        }
           

        public void afterEndWait()
        {
            Application.Exit();
        }


        public void panelToggle(Panel active,Panel toBeActive)
        {
            active.Visible = false;

            toBeActive.Visible = true;
            toBeActive.Location = new System.Drawing.Point(0, 0);
            toBeActive.Size = new System.Drawing.Size(300, 430);
            CurrentVisiblePanel = toBeActive;
        
        }

        public void addCommonControl(Panel toBeActive, Label Ccontrol)
        {
            toBeActive.Controls.Add(Ccontrol);
            
        }




        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            selectedArea = AreaComboBox.Items[AreaComboBox.SelectedIndex].ToString();
            this.panelToggle(Select_area_panel,Select_category_panel);
            this.addCommonControl(Select_category_panel, ExitLabel);
            this.addCommonControl(Select_category_panel,Admin_panel_label);
        }
        private void Category_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            selectedCategory = Category_combo_box.Items[Category_combo_box.SelectedIndex].ToString();

            this.panelToggle(Select_category_panel, Result_panel);
            this.addCommonControl(Result_panel, ExitLabel);


            Result_grid_view.DataSource = dctx.ItemTables.Where(X=> X.Area == selectedArea && X.Category== selectedCategory);
            foreach (DataGridViewColumn col in Result_grid_view.Columns)
            {
                
                if (col.HeaderText == "Address")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "Phone_number")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "Area")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "Category")
                {
                    col.Visible = false;

                }
                if (col.HeaderText == "Latitude")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "Longitude")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "AreaTable")
                {
                    col.Visible = false;
                }
                if (col.HeaderText == "CategoryTable")
                {
                    col.Visible = false;
                }
            }
            
        }

        private void ExitLabel_Click(object sender, EventArgs e)
        {
            
            this.panelToggle(CurrentVisiblePanel,ExitPanel);
            this.delayedWork(new workAfterDelay(afterEndWait));
            
        }

        private void Search_new_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(Result_panel,Select_area_panel);
            this.addCommonControl(Select_area_panel, ExitLabel);
            this.addCommonControl(Select_area_panel, Admin_panel_label);
        }

        private void Show_detail_button_Click(object sender, EventArgs e)
        {
            if (Result_grid_view.SelectedCells.Count > 0)
            {
                int selectedrowindex = Result_grid_view.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = Result_grid_view.Rows[selectedrowindex];

                string a = Convert.ToString(selectedRow.Cells["Id"].Value);

                selecteditemID = Int32.Parse(a);
                this.panelToggle(Result_panel, Result_show_panel);
                this.addCommonControl(Result_show_panel, ExitLabel);

                ItemTable i = dctx.ItemTables.SingleOrDefault(x => x.Id == selecteditemID);
                name_show_label.Text = i.Name;
                PN_show_label.Text = i.Phone_number.ToString();
                Adress_show_label.Text = i.Address;
                area_show_label.Text = i.Area;
                distance_show_label.Text = "Not Available";


            }
            else
            {
                MessageBox.Show("Select a Id for details", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            
        }

        private void back_area_label_Click(object sender, EventArgs e)
        {
            this.panelToggle(Select_category_panel,Select_area_panel);
            this.addCommonControl(Select_area_panel, ExitLabel);
            this.addCommonControl(Select_area_panel, Admin_panel_label);
        }

        private void Back_resultPnael_label_Click(object sender, EventArgs e)
        {
            this.panelToggle(Result_show_panel, Result_panel);
            this.addCommonControl(Result_panel, ExitLabel);
        }

        private void Admin_panel_label_Click(object sender, EventArgs e)
        {
            Loginframe lgf = new Loginframe(this);
            this.Enabled = false;
            lgf.Show();
        }

        public void ifLogin()
        {
            this.panelToggle(CurrentVisiblePanel,admin_panel);
            this.addCommonControl(admin_panel, ExitLabel);
        }

        private void Logout_label_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Select_area_panel);
            this.addCommonControl(Select_area_panel, ExitLabel);
            this.addCommonControl(Select_area_panel, Admin_panel_label);
        }

        private void item_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Item_edit_panel);
            this.addCommonControl(Item_edit_panel, ExitLabel);
            this.addCommonControl(Item_edit_panel, Logout_label);
            this.addCommonControl(Item_edit_panel, backTo_Item_edit_panel_label);

        }

        private void catagory_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Category_edit_panel);
            this.addCommonControl(Category_edit_panel, ExitLabel);
            this.addCommonControl(Category_edit_panel, Logout_label);
            this.addCommonControl(Category_edit_panel, backTo_Item_edit_panel_label);
        }

        private void area_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Area_edit_panel);
            this.addCommonControl(Area_edit_panel, ExitLabel);
            this.addCommonControl(Area_edit_panel, Logout_label);
            this.addCommonControl(Area_edit_panel, backTo_Item_edit_panel_label);
        }

        private void Category_combo_box_for_delete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backTo_Item_edit_panel_label_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, admin_panel);
            this.addCommonControl(admin_panel, ExitLabel);
            this.addCommonControl(admin_panel, Logout_label);
        }

        private void Category_add_button_Click(object sender, EventArgs e)
        {
            try
            {


                CategoryTable ct = new CategoryTable();
                ct.Name = Category_name_field_for_add.Text;
                dctx.CategoryTables.InsertOnSubmit(ct);
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                } 
                this.refreshlist();
                MessageBox.Show("New Category Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            
        }

        private void Category_modify_button_Click(object sender, EventArgs e)
        {
            try
            {


                CategoryTable ct = dctx.CategoryTables.SingleOrDefault(x => x.Name == Category_combo_box_for_modify.Items[Category_combo_box_for_modify.SelectedIndex].ToString()); ;
                
                CategoryTable newCT = new CategoryTable();
                newCT.Name = Category_name_field_for_modify.Text;
                dctx.CategoryTables.InsertOnSubmit(newCT);
                dctx.CategoryTables.DeleteOnSubmit(ct);
                
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException) {
                } 
                this.refreshlist();
                MessageBox.Show("Category Modified", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show(r.StackTrace);
                MessageBox.Show(r.Message);
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            
        }

        private void Category_delete_button_Click(object sender, EventArgs e)
        {
            try
            {


                CategoryTable ct = dctx.CategoryTables.SingleOrDefault(x => x.Name == Category_combo_box_for_delete.Text); ;
                dctx.CategoryTables.DeleteOnSubmit(ct);
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                } 
                this.refreshlist();
                MessageBox.Show("Category Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
           
        }

        private void area_add_button_Click(object sender, EventArgs e)
        {
            try
            {


                AreaTable at = new AreaTable();
                at.Name = Area_name_field_for_add.Text;
                dctx.AreaTables.InsertOnSubmit(at);
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                } 
                this.refreshlist();
                MessageBox.Show("New Area Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }

            
        }

        private void area_modify_button_Click(object sender, EventArgs e)
        {
            try
            {


                AreaTable ct = dctx.AreaTables.SingleOrDefault(x => x.Name == Area_combobox_fo_modify.Items[Area_combobox_fo_modify.SelectedIndex].ToString()); ;

                AreaTable newCT = new AreaTable();
                newCT.Name = Area_name_field_for_modify.Text;
                dctx.AreaTables.InsertOnSubmit(newCT);
                dctx.AreaTables.DeleteOnSubmit(ct);
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                } 
                this.refreshlist();
                MessageBox.Show("Area Modified", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            
        }

        private void area_delete_button_Click(object sender, EventArgs e)
        {
            try
            {


                AreaTable ct = dctx.AreaTables.SingleOrDefault(x => x.Name == Area_combobox_fo_delete.Text); ;
                dctx.AreaTables.DeleteOnSubmit(ct);
                try
                {
                    dctx.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                } 
                this.refreshlist();
                MessageBox.Show("Area Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
           
        }

        private void Item_add_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Item_add_panel);
            this.addCommonControl(Item_add_panel, ExitLabel);
            this.addCommonControl(Item_add_panel, Logout_label);
            this.addCommonControl(Item_add_panel, Back_To_previous);
        }

        private void Item_delete_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Item_delete_panel);
            this.addCommonControl(Item_delete_panel, ExitLabel);
            this.addCommonControl(Item_delete_panel, Logout_label);
            this.addCommonControl(Item_delete_panel, Back_To_previous);
        }

        private void Item_modify_button_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Item_modify_panel);
            this.addCommonControl(Item_modify_panel, ExitLabel);
            this.addCommonControl(Item_modify_panel, Logout_label);
            this.addCommonControl(Item_modify_panel, Back_To_previous);
        }

        private void Back_to_item_edit_panel_label_Click(object sender, EventArgs e)
        {
            this.panelToggle(CurrentVisiblePanel, Item_edit_panel);
            this.addCommonControl(Item_edit_panel, ExitLabel);
            this.addCommonControl(Item_edit_panel, Logout_label);
            this.addCommonControl(Item_edit_panel, backTo_Item_edit_panel_label);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Item_delete_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Welcome_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ExitPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void endPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void admin_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void select_what_label_Click(object sender, EventArgs e)
        {

        }

        private void welcome_label_Click(object sender, EventArgs e)
        {

        }

        private void Result_show_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void distance_show_label_Click(object sender, EventArgs e)
        {

        }

        private void distance_label_Click(object sender, EventArgs e)
        {

        }

        private void area_show_label_Click(object sender, EventArgs e)
        {

        }

        private void Area_label_Click(object sender, EventArgs e)
        {

        }

        private void Adress_show_label_Click(object sender, EventArgs e)
        {

        }

        private void Adress_label_Click(object sender, EventArgs e)
        {

        }

        private void PN_show_label_Click(object sender, EventArgs e)
        {

        }

        private void PN_label_Click(object sender, EventArgs e)
        {

        }

        private void name_show_label_Click(object sender, EventArgs e)
        {

        }

        private void name_label_Click(object sender, EventArgs e)
        {

        }

        private void Result_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Result_grid_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Result_found_label_Click(object sender, EventArgs e)
        {

        }

        private void Select_category_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Select_area_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Item_edit_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Item_edit_label_Click(object sender, EventArgs e)
        {

        }

        private void Category_edit_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Category_combo_box_for_modify_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category_name_field_for_modify.Enabled = true;
        }

        private void Category_name_field_for_modify_TextChanged(object sender, EventArgs e)
        {

        }

        private void Category_name_field_for_add_TextChanged(object sender, EventArgs e)
        {

        }

        private void Category_name_label_for_Mdify_Click(object sender, EventArgs e)
        {

        }

        private void Category_name_label_for_add_Click(object sender, EventArgs e)
        {

        }

        private void Area_edit_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Area_combobox_fo_delete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Area_combobox_fo_modify_SelectedIndexChanged(object sender, EventArgs e)
        {
            Area_name_field_for_modify.Enabled = true;
        }

        private void Area_name_field_for_modify_TextChanged(object sender, EventArgs e)
        {

        }

        private void Area_name_field_for_add_TextChanged(object sender, EventArgs e)
        {

        }

        private void Area_name_label_for_modify_Click(object sender, EventArgs e)
        {

        }

        private void Area_name_label_for_add_Click(object sender, EventArgs e)
        {

        }

        private void Item_add_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Item_modify_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void item_add_category_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void item_add_pn_field_TextChanged(object sender, EventArgs e)
        {

        }

        private void item_add_pn_label_Click(object sender, EventArgs e)
        {

        }

        private void item_add_address_field_TextChanged(object sender, EventArgs e)
        {

        }

        private void item_add_adress_label_Click(object sender, EventArgs e)
        {

        }

        private void Item_finally_add_button_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                 
                
                ItemTable i = new ItemTable();
                i.Area = item_add_area_box.Items[item_add_area_box.SelectedIndex].ToString();
                i.Category = item_add_category_box.Items[item_add_category_box.SelectedIndex].ToString();
                i.Name = item_add_name_field.Text;
                i.Address = item_add_address_field.Text;
                i.Phone_number = Int32.Parse(item_add_pn_field.Text);

                dctx.ItemTables.InsertOnSubmit(i);
                dctx.SubmitChanges();
                MessageBox.Show("Item Added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemTable i = dctx.ItemTables.SingleOrDefault(X => X.Id == selecteditemID);
            dctx.ItemTables.DeleteOnSubmit(i);
            dctx.SubmitChanges();
            MessageBox.Show("Item Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                selecteditemID = Int32.Parse(item_delete_id_field.Text);
                ItemTable i = dctx.ItemTables.SingleOrDefault(X=>X.Id==selecteditemID);
                item_delete_area_box.Text = i.Area;
                item_delete_cateegory_box.Text = i.Category;
                item_delete_name_field.Text = i.Name;
                item_delete_pn_field.Text = i.Phone_number.ToString();
                item_delete_adress_field.Text = i.Address;

              

            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void item_modify_confirm_button_Click(object sender, EventArgs e)
        {
            ItemTable i = dctx.ItemTables.SingleOrDefault(X => X.Id == selecteditemID);
            ItemTable newI = new ItemTable();
            newI.Id = selecteditemID;
            newI.Name = item_modify_name_field.Text;
            newI.Address = item_modify_adress_field.Text;
            newI.Phone_number = Int32.Parse(item_modify_pn_field.Text);
            newI.Area = item_modify_area_box.Text;
            newI.Category = item_modify_category_box.Text;
            dctx.ItemTables.DeleteOnSubmit(i);
            dctx.ItemTables.InsertOnSubmit(newI);
            dctx.SubmitChanges();
            MessageBox.Show("item modified", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void item_modify_find_button_Click(object sender, EventArgs e)
        {
            item_modify_area_box.Enabled = true;
            item_modify_category_box.Enabled = true;
            item_modify_name_field.Enabled = true;
            item_modify_pn_field.Enabled = true;
            item_modify_adress_field.Enabled = true;

            try
            {


                selecteditemID = Int32.Parse(item_modify_id_field.Text);
                ItemTable i = dctx.ItemTables.SingleOrDefault(X => X.Id == selecteditemID);
                item_modify_area_box.Text = i.Area;
                item_modify_category_box.Text = i.Category;
                item_modify_name_field.Text = i.Name;
                item_modify_pn_field.Text = i.Phone_number.ToString();
                item_modify_adress_field.Text = i.Address;
               



            }
            catch (Exception r)
            {
                MessageBox.Show("Insert all field Correctly", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            }
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            downPoint = new Point(e.X, e.Y);

        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - downPoint.X, p.Y - downPoint.Y);
            }

        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

        }




    }
}
