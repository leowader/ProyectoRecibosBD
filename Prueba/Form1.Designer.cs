namespace Prueba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnconectar = new System.Windows.Forms.Button();
            this.grillaescuela = new System.Windows.Forms.DataGridView();
            this.btnestado = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grillaescuela)).BeginInit();
            this.SuspendLayout();
            // 
            // btnconectar
            // 
            this.btnconectar.Location = new System.Drawing.Point(193, 121);
            this.btnconectar.Name = "btnconectar";
            this.btnconectar.Size = new System.Drawing.Size(75, 23);
            this.btnconectar.TabIndex = 0;
            this.btnconectar.Text = "conectar";
            this.btnconectar.UseVisualStyleBackColor = true;
            this.btnconectar.Click += new System.EventHandler(this.btnconectar_Click);
            // 
            // grillaescuela
            // 
            this.grillaescuela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grillaescuela.Location = new System.Drawing.Point(12, 185);
            this.grillaescuela.Name = "grillaescuela";
            this.grillaescuela.Size = new System.Drawing.Size(453, 150);
            this.grillaescuela.TabIndex = 1;
            // 
            // btnestado
            // 
            this.btnestado.Location = new System.Drawing.Point(299, 121);
            this.btnestado.Name = "btnestado";
            this.btnestado.Size = new System.Drawing.Size(75, 23);
            this.btnestado.TabIndex = 2;
            this.btnestado.Text = "estado";
            this.btnestado.UseVisualStyleBackColor = true;
            this.btnestado.Click += new System.EventHandler(this.btnestado_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 412);
            this.Controls.Add(this.btnestado);
            this.Controls.Add(this.grillaescuela);
            this.Controls.Add(this.btnconectar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grillaescuela)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnconectar;
        private System.Windows.Forms.DataGridView grillaescuela;
        private System.Windows.Forms.Button btnestado;
    }
}

