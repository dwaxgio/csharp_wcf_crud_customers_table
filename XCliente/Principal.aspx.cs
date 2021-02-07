using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCliente
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid(); // Se llena el gridview al momento de cargar la página
            }
        }

        private void BindGrid()
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            GridView1.DataSource = client.Get().CustomersTable;
            GridView1.DataBind(); // Une el datasource con el gridview
        }

        protected void Insert(object sender, EventArgs e)
        {
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            client.Insert(txtName.Text.Trim(), txtCountry.Text.Trim()); // Toma datos de txt, y los envía como parámetros al metodo insert del servicio
            this.BindGrid(); // Vuelve a llenar datagridview
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int customerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string name = (row.FindControl("txtName") as TextBox).Text;
            string country = (row.FindControl("txtCountry") as TextBox).Text;
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            client.Update(customerId, name, country);
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            client.Delete(customerId);
            this.BindGrid();
        }

        // Mensaje de confirmación cuando se elimina un registro
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
            {
                (e.Row.Cells[2].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
        }
    }
}