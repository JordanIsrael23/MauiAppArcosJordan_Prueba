namespace MauiAppArcosJordan_Prueba
{
    public partial class MainPage : ContentPage
    {
        private Productos productos;

        public MainPage()
        {
            InitializeComponent();
            productos = new Productos();
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var nuevoProductto = productos.Create(
                int.Parse(txtId.Text),
                txtNombre.Text,
                int.Parse(txtCantidad.Text),
                txtDescripcion.Text,
                txtCaregoria.Text
            );

            DisplayAlertAsync("Producto Creado",$"Producto {nuevoProductto.nombre} - {nuevoProductto.id}", "OK"); 
        }

        private void btnLeer_Clicked(object sender, EventArgs e)
        {
            var producto = productos.ReadById(int.Parse(txtId.Text));
            if (producto != null)
            {
                txtNombre.Text = producto.nombre;
                txtCantidad.Text = producto.cantidad.ToString();
                txtDescripcion.Text = producto.descripcion;
                txtCaregoria.Text = producto.categoria;

                DisplayAlertAsync("Datos Recuperados", $"Producto con ID {txtId.Text}", "OK");
            }
            else
            {
                DisplayAlertAsync("Producto No Existe", $"Producto con ID {txtId.Text}", "OK");

                txtId.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtCaregoria.Text = string.Empty;
            }
        }

        private void btnActualziar_Clicked(object sender, EventArgs e)
        {
            try
            {
                productos.Update(
                    int.Parse(txtId.Text),
                    txtNombre.Text,
                    int.Parse(txtCantidad.Text),
                    txtDescripcion.Text,
                    txtCaregoria.Text
                );
                DisplayAlertAsync("Producto Actualizado", $"Producto:  {txtId.Text}", "OK");
            }
            catch(Exception ex)
            {
                DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }

        private void btnEliminiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                productos.Delete(int.Parse(txtId.Text));

                DisplayAlertAsync("Producto Eliminado", $"Producto con ID {txtId.Text} eliminado", "OK");

                txtId.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtCaregoria.Text = string.Empty;
                
            }
            catch(Exception ex)
            {
                DisplayAlertAsync("Error", ex.Message, "OK");
                txtId.Text = string.Empty;
            }
        }
    }
}
