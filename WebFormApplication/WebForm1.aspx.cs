using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormApplication.ServiceReference1;

namespace WebFormApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = null;
                GridView1.DataSource = service.getStudents();
                GridView1.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string name = txtName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (btn_Submit.Text == "Submit")
            {
                bool isInsert = service.insert(name, phone, email, password);
                if (isInsert)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Save Successfully')", true);
                    GridView1.DataSource = null;
                    GridView1.DataSource = service.getStudents();
                    GridView1.DataBind();
                    txtID.Text =  "";
                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Error')", true);
                }
            }
            else if(btn_Submit.Text=="Save Changes")
            {
                bool isUpdate = service.update(id,name, phone, email, password);
                if (isUpdate)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Updated Successfully')", true);
                    GridView1.DataSource = null;
                    GridView1.DataSource = service.getStudents();
                    GridView1.DataBind();
                    txtID.Text = "";
                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Error')", true);
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int id = int.Parse(row.Cells[0].Text);
            bool isDelete = service.delete(id);
            if (isDelete)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Deleted Successfully')", true);
                GridView1.DataSource = null;
                GridView1.DataSource = service.getStudents();
                GridView1.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "callfunction", "alert('Error')", true);
            }
        }

        protected void Btn_Edit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            int id = int.Parse(row.Cells[0].Text);
            Student obj = service.getSingleRecord(id);
            txtID.Text = obj.Id + "";
            txtName.Text = obj.Name;
            txtPhone.Text = obj.Phone;
            txtEmail.Text = obj.Email;
            txtPassword.Text = obj.Password;
            btn_Submit.Text = "Save Changes";
        }
    }
}