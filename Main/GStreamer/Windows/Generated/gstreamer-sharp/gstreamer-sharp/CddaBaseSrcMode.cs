// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst.Cdda {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[Gst.GLib.GType (typeof (Gst.Cdda.CddaBaseSrcModeGType))]
	public enum CddaBaseSrcMode {

		Normal,
		Continuous,
	}

	internal class CddaBaseSrcModeGType {
		[DllImport ("libgstcdda-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_cdda_base_src_mode_get_type ();

		public static Gst.GLib.GType GType {
			get {
				return new Gst.GLib.GType (gst_cdda_base_src_mode_get_type ());
			}
		}
	}
#endregion
}
