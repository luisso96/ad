using System;
using Gtk;

namespace SerpisAd
{
	public class WindowHelper
	{
		public static bool ConfirmDelete (Window window) {
			MessageDialog messageDialog = new MessageDialog (window,	//Ventana de la que nace
			                                                 DialogFlags.DestroyWithParent,	//Si se elimina la ventana padre se eliminara
			                                                 MessageType.Question,		//Tipo de ventana
			                                                 ButtonsType.YesNo,			//Tipo de botones
			                                                 "¿Seguro que quieres eliminar el elemento seleccionado?");

			messageDialog.Title = window.Title;
			ResponseType response = (ResponseType)messageDialog.Run();
			messageDialog.Destroy ();
			return response == ResponseType.Yes;
		}
	}
}

