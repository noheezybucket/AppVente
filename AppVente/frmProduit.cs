using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppVente.Modell;

namespace AppVente
{
    public partial class frmProduit : Form
    {
        bdVenteEntities db = new bdVenteEntities();
        public frmProduit()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void frmProduit_Load(object sender, EventArgs e)
        {
            dgProduit.DataSource = db.Produit.ToList();
            cbbCategorie.DataSource = db.Categorie.ToList();
            cbbCategorie.ValueMember = "CodeCategorie";
            cbbCategorie.DisplayMember = "Libelle";

        }

        private void resetForm()
        {
            txtCode.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtPU.Text = string.Empty;
            txtQteCri.Text = string.Empty;
            txtQteMin.Text = string.Empty;
            dgProduit.DataSource = db.Produit.ToList();
            cbbCategorie.DataSource = db.Categorie.ToList();
            cbbCategorie.ValueMember = "CodeCategorie";
            cbbCategorie.DisplayMember = "LibelleCategorie";
            txtCode.Focus();
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            Produit p = new Produit();
            p.CodeProduit = txtCode.Text;
            p.DesignationProduit = int.Parse(txtDesignation.Text);
            p.PU = int.Parse(txtPU.Text);
            p.QteMin = int.Parse(txtQteMin.Text);
            p.QteCri = int.Parse(txtQteCri.Text);
            p.CodeCategorie = cbbCategorie.SelectedValue.ToString();

            db.Produit.Add(p);
            db.SaveChanges();

            resetForm();        }

        private void btnSelectionner_Click(object sender, EventArgs e)
        {
            txtCode.Text = dgProduit.CurrentRow.Cells[1].Value.ToString();
            txtDesignation.Text = dgProduit.CurrentRow.Cells[2].Value.ToString();
            txtPU.Text = dgProduit.CurrentRow.Cells[3].Value.ToString();
            txtQteMin.Text = dgProduit.CurrentRow.Cells[4].Value.ToString();
            txtQteCri.Text = dgProduit.CurrentRow.Cells[5].Value.ToString();
            cbbCategorie.SelectedValue = dgProduit.CurrentRow.Cells[6].Value;
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgProduit.CurrentRow.Cells[0].Value.ToString());
            Produit p = db.Produit.Find(id);

            p.CodeProduit = txtCode.Text;
            p.DesignationProduit = int.Parse(txtDesignation.Text);
            p.PU = int.Parse(txtPU.Text);
            p.QteMin = int.Parse(txtQteMin.Text);
            p.QteCri = int.Parse(txtQteCri.Text);
            p.CodeCategorie = cbbCategorie.SelectedValue.ToString();

            db.SaveChanges();

            resetForm();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            int? id = int.Parse(dgProduit.CurrentRow.Cells[0].Value.ToString());
            Produit p = db.Produit.Find(id);
            db.Produit.Remove(p);
            db.SaveChanges();

            resetForm();
        }

        private void dgProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            var liste = db.Produit.ToList();
            if(!string.IsNullOrEmpty(txtRCode.Text) )
            {
                liste = liste.Where(a=>a.CodeProduit.ToUpper().Contains(txtRCode.Text.ToUpper())).ToList();
            }
            
           

            dgProduit.DataSource = liste;
        }
    }
}
