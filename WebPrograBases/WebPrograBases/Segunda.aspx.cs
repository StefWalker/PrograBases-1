﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebPrograBases
{
    public partial class Segunda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
                Response.Redirect("login.aspx");
            lblUserDetails.Text = "Username : " + Session["username"];
            Session["idPersona"] = optenerIDPersona();
            lblIdUsuario.Text = "ID : " + Session["idPersona"];
            Session["numCuenta"] = optenerNumCuenta();
            lblNumCuenta.Text = "Cuenta : " + Session["numCuenta"];
        }

        protected void btnEstadosCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("MostrarCuenta.aspx");
        }

        protected void btnBeneficiarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("MostrarBeneficiarios.aspx");
        }

        protected int optenerIDPersona()
        {
            int numID = 0;
            using (SqlConnection sqlCon = new SqlConnection("Initial Catalog = PrograBases; Data Source=localhost;Integrated Security=SSPI;"))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("VerDocumentoIdentidad", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@inUsuario", SqlDbType.NVarChar).Value = Session["username"].ToString();
                numID = Convert.ToInt32(sql_cmnd.ExecuteScalar());
                sqlCon.Close();
            }
            return numID;
        }
        
        protected int optenerNumCuenta()
        {
            int numCuenta = 0;
            using (SqlConnection sqlCon = new SqlConnection("Initial Catalog = PrograBases; Data Source=localhost;Integrated Security=SSPI;"))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("VerNumeroCuenta", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@inValorDocumentoIdentidad", SqlDbType.Int).Value = Session["idPersona"].ToString();
                numCuenta = Convert.ToInt32(sql_cmnd.ExecuteScalar());
                sqlCon.Close();
            }
            return numCuenta;
        }

        protected void btnAgregarBeneficiarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarBeneficiario.aspx");
        }
    }
}