using ControlProteinasClient.ServicioControlProteinas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlProteinasClient
{
    public partial class ControlProteinasForm : Form
    {
        private ProteinTrackingServiceSoapClient service =
            new ProteinTrackingServiceSoapClient();

        private User[] users;
        public ControlProteinasForm()
        {
            InitializeComponent();
        }

        private void Onload(object sender, EventArgs e)
        {
            users = service.ListUsers();
            cmbUser.DataSource = users;
            cmbUser.DisplayMember = "Name";
            cmbUser.ValueMember = "UserId";
        }

        private void OnUserChanged(object sender, EventArgs e)
        {
            var index = cmbUser.SelectedIndex;
            lblTotal.Text = users[index].Total.ToString();
            lblMeta.Text = users[index].Goal.ToString();
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            service.AddUser(txtUsuario.Text, int.Parse(txtMeta.Text));
            users = service.ListUsers();
            cmbUser.DataSource = users;
        }

        private void OnAddProtein(object sender, EventArgs e)
        {
            var userId = users[cmbUser.SelectedIndex].UserId;
            var newTotal = service.AddProtein(int.Parse(txtAgregar.Text), userId);
            users[cmbUser.SelectedIndex].Total = newTotal;
            lblTotal.Text = newTotal.ToString();
        }
    }
}
